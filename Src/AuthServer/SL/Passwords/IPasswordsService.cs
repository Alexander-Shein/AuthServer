using System.Threading.Tasks;
using AuthGuard.SL.Passwordless.Models;
using DddCore.Contracts.BLL.Errors;

namespace AuthGuard.SL.Passwords
{
    public interface IPasswordsService
    {
        Task<OperationResult> ForgotPasswordAsync(CallbackUrlAndUserNameIm im);
        Task<OperationResult> ResetPasswordAsync(ResetPasswordIm im);
        Task<OperationResult> ChangePasswordAsync(ChangePasswordIm im);
        Task<OperationResult> AddPassword(PasswordIm im);
    }
}