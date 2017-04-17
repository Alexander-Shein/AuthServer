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
        Task<UserVm> UpdateAsync(ClaimsPrincipal claims, UserIm im);
        Task<IdentityResult> ConfirmAccountAsync(ConfirmAccountIm im);
    }

    public class UsersService : IUsersService
    {
        #region Private Members

        private readonly ApplicationDbContext applicationDbContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        #endregion

        public UsersService(
            ApplicationDbContext applicationDbContext,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        #region Public Methods

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

        public async Task<UserVm> UpdateAsync(ClaimsPrincipal claims, UserIm im)
        {
            var user = await userManager.GetUserAsync(claims);
            if (user == null) throw new ArgumentException();

            await UpdateEmailAsync(user, im.Email, im.EmailCode);
            await UpdatePhoneAsync(user, im.PhoneNumber, im.PhoneNumberCode);

            if (im.IsTwoFactorEnabled.HasValue && user.TwoFactorEnabled != im.IsTwoFactorEnabled)
            {
                var result = await userManager.SetTwoFactorEnabledAsync(user, im.IsTwoFactorEnabled.Value);
                await signInManager.SignInAsync(user, isPersistent: false);
                if (result.Succeeded)
                {
                    user.TwoFactorEnabled = im.IsTwoFactorEnabled.Value;
                }
                else
                {
                    throw new ArgumentException(result.Errors.First().Description);
                }
            }

            var vm = Map(user);
            return vm;
        }

        public async Task<IdentityResult> ConfirmAccountAsync(ConfirmAccountIm im)
        {
            var user = await userManager.FindByIdAsync(im.UserId.ToString());

            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "1",
                    Description = $"User with id '{im.UserId}' does not exist."
                });
            }

            if (String.Equals(im.Provider, "Email", StringComparison.OrdinalIgnoreCase))
            {
                return await userManager.ConfirmEmailAsync(user, im.Code);
            }
            else if (String.Equals(im.Provider, "Phone", StringComparison.OrdinalIgnoreCase))
            {
                return await userManager.ChangePhoneNumberAsync(user, user.PhoneNumber, im.Code);
            }
            else
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "2",
                    Description = $"Provider with name '{im.Provider}' provider is not valid."
                });
            }
        }

        #endregion

        #region Private Methods

        private async Task UpdatePhoneAsync(ApplicationUser user, string phone, string code)
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

        private async Task UpdateEmailAsync(ApplicationUser user, string email, string code)
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
                Id = new Guid(user.Id),
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

        #endregion
    }
}