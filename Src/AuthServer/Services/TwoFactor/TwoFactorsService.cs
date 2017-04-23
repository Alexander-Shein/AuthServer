using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthGuard.BLL.Domain.Entities;
using AuthGuard.Services.TwoFactor.Models.Input;
using AuthGuard.Services.TwoFactor.Models.View;
using DddCore.Contracts.BLL.Errors;
using Microsoft.AspNetCore.Identity;

namespace AuthGuard.Services.TwoFactor
{
    public class TwoFactorsService : ITwoFactorsService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IEmailSender emailSender;
        private readonly ISmsSender smsSender;

        public TwoFactorsService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ISmsSender smsSender)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailSender = emailSender;
            this.smsSender = smsSender;
        }

        public async Task<(IEnumerable<TwoFactorProviderVm> Providers, OperationResult OperationResult)> GetTwoFactorProvidersAsync()
        {
            var user = await signInManager.GetTwoFactorAuthenticationUserAsync();

            if (user == null)
            {
                return (null, OperationResult.FailedResult(1, "User is not logged in for 2 factor validation."));
            }

            var userFactors = await userManager.GetValidTwoFactorProvidersAsync(user);
            var providers = userFactors.Select(x => new TwoFactorProviderVm
            {
                DisplayName = x,
                Key = x
            });

            return (providers, OperationResult.SucceedResult);
        }

        public async Task<OperationResult> SendCodeAsync(TwoFactorProviderIm im)
        {
            var provider = im.Key;

            var user = await signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                return OperationResult.FailedResult(2, "User is not logged in for 2 factor validation.");
            }

            var code = await userManager.GenerateTwoFactorTokenAsync(user, provider);

            var parameters = new Dictionary<string, string>
            {
                {"Code", code}
            };

            if (String.Equals(provider, "Email", StringComparison.OrdinalIgnoreCase))
            {
                await emailSender.SendEmailAsync(await userManager.GetEmailAsync(user), Template.TwoFactorCode, parameters);
            }

            if (String.Equals(provider, "Phone", StringComparison.OrdinalIgnoreCase))
            {
                await smsSender.SendSmsAsync(await userManager.GetPhoneNumberAsync(user), Template.TwoFactorCode, parameters);
            }

            return OperationResult.SucceedResult;
        }

        public async Task<OperationResult> VerifyCode(TwoFactorVerificationIm im)
        {
            var signInResult = await signInManager.TwoFactorSignInAsync(im.TwoFactorProviderKey, im.Code, im.RememberLogIn, im.RememberBrowser);

            if (signInResult.IsLockedOut)
            {
                return OperationResult.FailedResult(3, "Account is locked.");
            }

            return OperationResult.SucceedResult;
        }
    }
}