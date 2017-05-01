using System.Threading.Tasks;
using AuthGuard.SL.Passwordless.Models;
using DddCore.Contracts.BLL.Errors;
using DddCore.Contracts.SL.Services.Application;

namespace AuthGuard.SL.Passwordless.Workflow
{
    public interface IPasswordlessWorkflowService : IWorkflowService
    {
        Task<OperationResult> SendLogInLinkAsync(CallbackUrlAndUserNameIm im);
        Task<OperationResult> SendSignUpLinkAsync(CallbackUrlAndUserNameIm im);

        Task<OperationResult> SignUpAsync(CodeIm im);
        Task<OperationResult> LogInAsync(CodeIm im);
    }
}