using System.Collections.Generic;
using System.Threading.Tasks;
using AuthGuard.SL.TwoFactor.Models.Input;
using AuthGuard.SL.TwoFactor.Models.View;
using DddCore.Contracts.BLL.Errors;
using DddCore.Contracts.SL.Services.Application;

namespace AuthGuard.SL.TwoFactor
{
    public interface ITwoFactorsWorkflowService : IWorkflowService
    {
        Task<(IEnumerable<TwoFactorProviderVm> Providers, OperationResult OperationResult)> GetTwoFactorProvidersAsync();
        Task<OperationResult> SendCodeAsync(TwoFactorProviderIm im);
        Task<OperationResult> VerifyCode(TwoFactorVerificationIm im);
    }
}