using System.Threading.Tasks;
using AuthGuard.Api;
using AuthGuard.BLL.Domain.Entities;
using AuthGuard.Services.Users.Models.Input;
using AuthGuard.Services.Users.Models.View;
using DddCore.Contracts.BLL.Errors;

namespace AuthGuard.Services.Users
{
    public interface IUsersService
    {
        Task<ApplicationUser> GetUserByEmailOrPhoneAsync(string userName);
        Task<UserVm> GetCurrentUserAsync();
        Task<(UserVm User, OperationResult OperationResult)> UpdateAsync(UserIm im);
        Task<OperationResult> ConfirmAccountAsync(ConfirmAccountIm im);
        Task<OperationResult> SendCodeToAddLocalProvider(UserNameIm im);
    }
}