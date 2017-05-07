using System.Collections.Generic;
using System.Threading.Tasks;
using AuthGuard.Services.TwoFactor.Models.Input;
using AuthGuard.Services.TwoFactor.Models.View;
using DddCore.Contracts.BLL.Errors;
using DddCore.Contracts.SL.Services.Application;

namespace AuthGuard.Services.TwoFactor
{
    public interface ITwoFactorsWorkflowService : IWorkflowService
    {
        Task<(IEnumerable<TwoFactorProviderVm> Providers, OperationResult OperationResult)> GetTwoFactorProvidersAsync();
        Task<OperationResult> SendCodeAsync(TwoFactorProviderIm im);
        Task<OperationResult> VerifyCode(TwoFactorVerificationIm im);
    }
}