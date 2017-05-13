using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthGuard.BLL.Domain.Entities;
using AuthGuard.Data;
using AuthGuard.DAL.Repositories.Security;
using AuthGuard.SL.Notifications;
using AuthGuard.SL.Security;
using AuthGuard.SL.TwoFactor.Models.Input;
using AuthGuard.SL.TwoFactor.Models.View;
using DddCore.Contracts.BLL.Errors;
using Microsoft.AspNetCore.Identity;

namespace AuthGuard.SL.TwoFactor
{
    public class TwoFactorsWorkflowService : ITwoFactorsWorkflowService
    {
        readonly UserManager<ApplicationUser> userManager;
        readonly SignInManager<ApplicationUser> signInManager;
        readonly IEmailSender emailSender;
        readonly ISmsSender smsSender;
        readonly ISecurityCodesEntityService securityCodesService;
        readonly ApplicationDbContext context;
        readonly ISecurityCodesRepository securityCodesRepository;

        public TwoFactorsWorkflowService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ISmsSender smsSender,
            ISecurityCodesEntityService securityCodesService,
            ApplicationDbContext context,
            ISecurityCodesRepository securityCodesRepository)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailSender = emailSender;
            this.smsSender = smsSender;
            this.securityCodesService = securityCodesService;
            this.context = context;
            this.securityCodesRepository = securityCodesRepository;
        }

        public async Task<(IEnumerable<TwoFactorProviderVm> Providers, OperationResult OperationResult)> GetTwoFactorProvidersAsync()
        {
            var user = await signInManager.GetTwoFactorAuthenticationUserAsync();

            if (user == null)
            {
                return (null, OperationResult.Failed(1, "User is not logged in for 2 factor validation."));
            }

            var userFactors = await userManager.GetValidTwoFactorProvidersAsync(user);
            var providers = userFactors.Select(x => new TwoFactorProviderVm
            {
                DisplayName = x,
                Key = x
            });

            return (providers, OperationResult.Succeed);
        }

        public async Task<OperationResult> VerifyCode(TwoFactorVerificationIm im)
        {
            var user = await signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                return OperationResult.Failed(1, "User is not logged in for 2 factor validation.");
            }

            var securityCode = await securityCodesRepository.ReadByCodeAsync(im.Code);

            if (securityCode == null || user.Id != securityCode.GetParameterValue(SecurityCodeParameterName.UserId))
            {
                return OperationResult.Failed(2, "Invalid code.");
            }

            var provider = securityCode.GetParameterValue(SecurityCodeParameterName.LocalProvider);
            var code = await userManager.GenerateTwoFactorTokenAsync(user, provider);

            var signInResult = await signInManager.TwoFactorSignInAsync(provider, code, im.RememberLogIn, im.RememberBrowser);

            securityCodesService.Delete(securityCode);

            await context.SaveChangesAsync();

            if (signInResult.IsLockedOut)
            {
                return OperationResult.Failed(3, "Account is locked.");
            }

            return OperationResult.Succeed;
        }
    }
}