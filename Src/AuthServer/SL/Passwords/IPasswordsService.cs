using System.Threading.Tasks;
using AuthGuard.SL.Passwordless.Models;
using DddCore.Contracts.BLL.Errors;
using DddCore.Contracts.SL.Services.Application;

namespace AuthGuard.SL.Passwords
{
    public interface IPasswordsService : IWorkflowService
    {
        Task<OperationResult> ForgotPasswordAsync(CallbackUrlAndUserNameIm im);
        Task<OperationResult> ResetPasswordAsync(ResetPasswordIm im);
        Task<OperationResult> ChangePasswordAsync(ChangePasswordIm im);
        Task<OperationResult> AddPassword(PasswordIm im);
    }
}