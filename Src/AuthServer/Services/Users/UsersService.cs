using System;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthGuard.BLL.Domain.Entities;
using AuthGuard.Data;
using AuthGuard.Services.Security;
using AuthGuard.Services.Users.Models.Input;
using AuthGuard.Services.Users.Models.View;
using DddCore.Contracts.BLL.Errors;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthGuard.Services.Users
{
    public class UsersService : IUsersService
    {
        #region Private Members

        readonly ApplicationDbContext applicationDbContext;
        readonly UserManager<ApplicationUser> userManager;
        readonly SignInManager<ApplicationUser> signInManager;
        readonly ISecurityCodesService securityCodesService;

        #endregion

        public UsersService(
            ApplicationDbContext applicationDbContext,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ISecurityCodesService securityCodesService)
        {
            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.securityCodesService = securityCodesService;
        }

        #region Public Methods

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
                userName = ApplicationUser.CleanPhoneNumber(userName);
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

        public async Task<OperationResult> ConfirmAccountAsync(ConfirmAccountIm im)
        {
            var securityCode = await securityCodesService.Get(im.Code);

            if (securityCode == null || securityCode.SecurityCodeAction != SecurityCodeAction.ConfirmAccount)
            {
                return OperationResult.FailedResult(1, "Invalid code.");
            }

            //if (securityCode.ExpiredAt > DateTime.Now)
            //{
            //    securityCodesService.Delete(securityCode);
            //    await applicationDbContext.SaveChangesAsync();
            //    return OperationResult.FailedResult(2, "Code is expired.");
            //}

            var user = await userManager.FindByIdAsync(securityCode.UserId);

            if (String.Equals(im.Provider, "Email", StringComparison.OrdinalIgnoreCase))
            {
                user.EmailConfirmed = true;
            }
            else if (String.Equals(im.Provider, "Phone", StringComparison.OrdinalIgnoreCase))
            {
                user.PhoneNumberConfirmed = true;
            }
            else
            {
                return OperationResult.FailedResult(3, $"Provider with name '{im.Provider}' is not valid.");
            }

            securityCodesService.Delete(securityCode);
            applicationDbContext.Update(user);
            await applicationDbContext.SaveChangesAsync();
            await signInManager.SignInAsync(user, isPersistent: true);

            return OperationResult.SucceedResult;
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