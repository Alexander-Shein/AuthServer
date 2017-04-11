using IdentityServerWithAspNetIdentity.Models;
using System.Threading.Tasks;
using System;
using IdentityServerWithAspNetIdentity.Data;
using System.Linq.Expressions;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using IdentityServerWithAspNetIdentity.Services.Users.Models.View;

namespace IdentityServerWithAspNetIdentity.Services
{
    public interface IUsersService
    {
        Task<ApplicationUser> GetUserByEmailOrPhoneAsync(string userName);
        Task<UserVm> GetCurrentUserAsync(ClaimsPrincipal user);
        string CleanPhoneNumber(string phone);
    }

    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly UserManager<ApplicationUser> userManager;

        public UsersService(
            ApplicationDbContext applicationDbContext,
            UserManager<ApplicationUser> userManager)
        {
            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
        }

        public string CleanPhoneNumber(string phone)
        {
            if (String.IsNullOrWhiteSpace(phone)) return String.Empty;

            return new string(phone.Where(c => Char.IsDigit(c)).ToArray());
        }

        public async Task<UserVm> GetCurrentUserAsync(ClaimsPrincipal claims)
        {
            var user = await userManager.GetUserAsync(claims);
            if (user == null) return null;

            var vm = new UserVm
            {
                Email = user.Email,
                IsEmailConfirmed = user.EmailConfirmed,
                PhoneNumber = user.PhoneNumber,
                IsPhoneNumberConfirmed = user.PhoneNumberConfirmed,
                IsTwoFactorEnabled = user.TwoFactorEnabled,
                HasPassword = !String.IsNullOrEmpty(user.PasswordHash),
                ExternalProviders = user.Logins.Select(x => new UserExternalProviderVm
                {
                    Key = x.ProviderKey,
                    AuthenticationScheme = x.LoginProvider,
                    DisplayName = x.ProviderDisplayName
                })
            };

            return vm;
        }

        public async Task<ApplicationUser> GetUserByEmailOrPhoneAsync(string userName)
        {
            if (String.IsNullOrWhiteSpace(userName)) return null;

            var isEmail = userName.Contains("@");
            Expression<Func<ApplicationUser, bool>> predicate;

            if (!isEmail)
            {
                userName = CleanPhoneNumber(userName);
                predicate = x => x.PhoneNumber == userName;
            }
            else
            {
                predicate = x => x.Email == userName;
            }

            var user = await applicationDbContext.Set<ApplicationUser>().FirstOrDefaultAsync(predicate);
            return user;
        }
    }
}
