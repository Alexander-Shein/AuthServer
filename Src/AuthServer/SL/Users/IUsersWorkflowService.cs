using System.Threading.Tasks;
using AuthGuard.Api;
using AuthGuard.BLL.Domain.Entities;
using AuthGuard.SL.Users.Models.Input;
using AuthGuard.SL.Users.Models.View;
using DddCore.Contracts.BLL.Errors;
using DddCore.Contracts.SL.Services.Application;

namespace AuthGuard.SL.Users
{
    public interface IUsersWorkflowService : IWorkflowService
    {
        Task<ApplicationUser> GetUserByEmailOrPhoneAsync(string userName);
        Task<UserVm> GetCurrentUserAsync();
        Task<(UserVm User, OperationResult OperationResult)> UpdateAsync(UserIm im);
        Task<OperationResult> ConfirmAccountAsync(ConfirmAccountIm im);
        Task<OperationResult> SendCodeToAddLocalProvider(UserNameIm im);
    }
}