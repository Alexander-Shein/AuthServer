using System.Security.Claims;
using System.Threading.Tasks;
using AuthGuard.BLL.Domain.Entities;
using AuthGuard.Services.Users.Models.Input;
using AuthGuard.Services.Users.Models.View;
using Microsoft.AspNetCore.Identity;

namespace AuthGuard.Services.Users
{
    public interface IUsersService
    {
        Task<ApplicationUser> GetUserByEmailOrPhoneAsync(string userName);
        Task<UserVm> GetCurrentUserAsync(ClaimsPrincipal user);
        Task<UserVm> UpdateAsync(ClaimsPrincipal claims, UserIm im);
        Task<IdentityResult> ConfirmAccountAsync(ConfirmAccountIm im);
    }
}