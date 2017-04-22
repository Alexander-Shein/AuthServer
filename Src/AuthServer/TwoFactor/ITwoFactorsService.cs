using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthGuard.Data.Entities;
using AuthGuard.Services;
using AuthGuard.TwoFactor.Models.Input;
using AuthGuard.TwoFactor.Models.View;
using Microsoft.AspNetCore.Identity;

namespace AuthGuard.TwoFactor
{
    public interface ITwoFactorsService
    {
        Task<IEnumerable<TwoFactorProviderVm>> GetTwoFactorProvidersAsync();
        Task SendCodeAsync(TwoFactorProviderIm im);
        Task VerifyCode(TwoFactorVerificationIm im);
    }

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

        public async Task<IEnumerable<TwoFactorProviderVm>> GetTwoFactorProvidersAsync()
        {
            var user = await signInManager.GetTwoFactorAuthenticationUserAsync();
            var userFactors = await userManager.GetValidTwoFactorProvidersAsync(user);
            var providers = userFactors.Select(x => new TwoFactorProviderVm
            {
                DisplayName = x,
                Key = x
            });

            return providers;
        }

        public async Task SendCodeAsync(TwoFactorProviderIm im)
        {
            throw new System.NotImplementedException();
        }

        public Task VerifyCode(TwoFactorVerificationIm im)
        {
            throw new System.NotImplementedException();
        }
    }
}