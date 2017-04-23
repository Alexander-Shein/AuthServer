using System.Collections.Generic;
using System.Threading.Tasks;
using AuthGuard.Services.TwoFactor.Models.Input;
using AuthGuard.Services.TwoFactor.Models.View;
using DddCore.Contracts.BLL.Errors;

namespace AuthGuard.Services.TwoFactor
{
    public interface ITwoFactorsService
    {
        Task<(IEnumerable<TwoFactorProviderVm> Providers, OperationResult OperationResult)> GetTwoFactorProvidersAsync();
        Task<OperationResult> SendCodeAsync(TwoFactorProviderIm im);
        Task<OperationResult> VerifyCode(TwoFactorVerificationIm im);
    }
}