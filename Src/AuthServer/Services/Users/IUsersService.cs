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
using AuthServer.Services.Users.Models.Input;

namespace IdentityServerWithAspNetIdentity.Services
{
    public interface IUsersService
    {
        Task<ApplicationUser> GetUserByEmailOrPhoneAsync(string userName);
        Task<UserVm> GetCurrentUserAsync(ClaimsPrincipal user);
        string CleanPhoneNumber(string phone);
        Task<UserVm> Update(ClaimsPrincipal claims, UserIm im);
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

            var vm = Map(user);
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

        public async Task<UserVm> Update(ClaimsPrincipal claims, UserIm im)
        {
            var user = await userManager.GetUserAsync(claims);
            if (user == null) throw new ArgumentException();

            await UpdateEmail(user, im.Email, im.EmailCode);
            await UpdatePhone(user, im.PhoneNumber, im.PhoneNumberCode);

            if (im.IsTwoFactorEnabled.HasValue && user.TwoFactorEnabled != im.IsTwoFactorEnabled)
            {
                await userManager.SetTwoFactorEnabledAsync(user, im.IsTwoFactorEnabled.Value);
            }

            var vm = Map(user);
            return vm;
        }

        private async Task UpdatePhone(ApplicationUser user, string phone, string code)
        {
            if (phone == null) return;

            var userHasPhone = !String.IsNullOrEmpty(user.PhoneNumber);

            if (phone == String.Empty)
            {
                if (userHasPhone)
                {
                    await userManager.SetPhoneNumberAsync(user, null);
                }
            }
            else
            {
                if (String.Equals(user.PhoneNumber, phone, StringComparison.OrdinalIgnoreCase)) return;
                await userManager.ChangePhoneNumberAsync(user, phone, code);
            }
        }

        private async Task UpdateEmail(ApplicationUser user, string email, string code)
        {
            if (email == null) return;

            var userHasEmail = !String.IsNullOrEmpty(user.Email);

            if (email == String.Empty)
            {
                if (userHasEmail)
                {
                    await userManager.SetEmailAsync(user, null);
                }
            }
            else
            {
                if (String.Equals(user.Email, email, StringComparison.OrdinalIgnoreCase)) return;
                await userManager.ChangeEmailAsync(user, email, code);
            }
        }

        private UserVm Map(ApplicationUser user)
        {
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
    }
}
