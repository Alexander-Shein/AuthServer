using System.Threading.Tasks;
using AuthGuard.Services.Passwordless;
using DddCore.Contracts.BLL.Errors;

namespace AuthGuard.Services.Passwords
{
    public interface IPasswordsService
    {
        Task<OperationResult> ForgotPasswordAsync(CallbackUrlAndUserNameIm im);
        Task<OperationResult> ResetPasswordAsync(ResetPasswordIm im);
        Task<OperationResult> ChangePasswordAsync(ChangePasswordIm im);
        Task<OperationResult> AddPassword(PasswordIm im);
    }
}