using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AuthGuard.Api;
using AuthGuard.BLL.Domain.Entities;
using AuthGuard.Data;
using AuthGuard.Services.Security;
using AuthGuard.Services.Users.Models.Input;
using AuthGuard.Services.Users.Models.View;
using DddCore.Contracts.BLL.Errors;
using DddCore.Contracts.Crosscutting.UserContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthGuard.Services.Users
{
    public class UsersWorkflowService : IUsersWorkflowService
    {
        #region Private Members

        readonly ApplicationDbContext context;
        readonly UserManager<ApplicationUser> userManager;
        readonly SignInManager<ApplicationUser> signInManager;
        readonly ISecurityCodesService securityCodesService;
        readonly ISmsSender smsSender;
        readonly IEmailSender emailSender;
        readonly IUserContext<Guid> userContext;

        #endregion

        public UsersWorkflowService(
            ApplicationDbContext applicationDbContext,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ISecurityCodesService securityCodesService,
            ISmsSender smsSender,
            IEmailSender emailSender,
            IUserContext<Guid> userContext)
        {
            this.context = applicationDbContext;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.securityCodesService = securityCodesService;
            this.smsSender = smsSender;
            this.emailSender = emailSender;
            this.userContext = userContext;
        }

        #region Public Methods

        public async Task<UserVm> GetCurrentUserAsync()
        {
            var user = await context.Set<ApplicationUser>().FindAsync(userContext.Id.ToString());

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

            var user = await context.Set<ApplicationUser>().FirstOrDefaultAsync(predicate);
            return user;
        }

        public async Task<(UserVm User, OperationResult OperationResult)> UpdateAsync(UserIm im)
        {
            var user = await context.Set<ApplicationUser>().FindAsync(userContext.Id.ToString());

            var putEmailResult = await UpdateEmailAsync(user, im.Email, im.EmailCode ?? -1);

            if (putEmailResult.IsNotSucceed)
            {
                return (null, putEmailResult);
            }

            var putPhoneResult = await UpdatePhoneAsync(user, im.PhoneNumber, im.PhoneNumberCode ?? -1);

            if (putPhoneResult.IsNotSucceed)
            {
                return (null, putPhoneResult);
            }

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

            await context.SaveChangesAsync();

            return (vm, OperationResult.Succeed);
        }

        public async Task<OperationResult> SendCodeToAddLocalProvider(UserNameIm im)
        {
            var securityCode = SecurityCode.Generate(SecurityCodeAction.AddLocalProvider, SecurityCodeParameterName.UserName, im.UserName);
            securityCodesService.Insert(securityCode);

            var isEmail = im.UserName.Contains("@");

            var parameters = new Dictionary<string, string>
            {
                {"Code", securityCode.Code.ToString()}
            };

            if (isEmail)
            {
                await emailSender.SendEmailAsync(im.UserName, Template.AddLocalProvider, parameters);
            }
            else
            {
                var phone = ApplicationUser.CleanPhoneNumber(im.UserName);
                await smsSender.SendSmsAsync(phone, Template.AddLocalProvider, parameters);
            }

            await context.SaveChangesAsync();

            return OperationResult.Succeed;
        }

        public async Task<OperationResult> ConfirmAccountAsync(ConfirmAccountIm im)
        {
            var securityCode = await securityCodesService.Get(im.Code);

            if (securityCode == null || securityCode.SecurityCodeAction != SecurityCodeAction.ConfirmAccount)
            {
                return OperationResult.Failed(1, "Invalid code.");
            }

            //if (securityCode.ExpiredAt > DateTime.Now)
            //{
            //    securityCodesService.Delete(securityCode);
            //    await applicationDbContext.SaveChangesAsync();
            //    return OperationResult.FailedResult(2, "Code is expired.");
            //}

            var user = await userManager.FindByIdAsync(securityCode.GetParameterValue(SecurityCodeParameterName.UserId));

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
                return OperationResult.Failed(3, $"Provider with name '{im.Provider}' is not valid.");
            }

            securityCodesService.Delete(securityCode);
            context.Update(user);
            await context.SaveChangesAsync();
            await signInManager.SignInAsync(user, isPersistent: true);

            return OperationResult.Succeed;
        }

        #endregion

        #region Private Methods

        private async Task<OperationResult> UpdatePhoneAsync(ApplicationUser user, string phone, int code)
        {
            if (phone == null) return OperationResult.Succeed;

            var userHasPhone = !String.IsNullOrEmpty(user.PhoneNumber);

            if (phone == String.Empty)
            {
                if (userHasPhone)
                {
                    user.DeletePhone();
                }
            }
            else
            {
                if (String.Equals(user.PhoneNumber, phone, StringComparison.OrdinalIgnoreCase))
                {
                    return OperationResult.Succeed;
                }

                var securityCode = await securityCodesService.Get(code);
                securityCodesService.Delete(securityCode);
                

                if (
                    securityCode == null ||
                    !String.Equals(securityCode.GetParameterValue(SecurityCodeParameterName.UserName), phone, StringComparison.OrdinalIgnoreCase))
                {
                    return OperationResult.Failed(1, "Invalid code.");
                }

                user.PutConfirmedPhone(phone);
            }

            context.Update(user);
            return OperationResult.Succeed;
        }

        private async Task<OperationResult> UpdateEmailAsync(ApplicationUser user, string email, int code)
        {
            if (email == null) return OperationResult.Succeed;

            var userHasEmail = !String.IsNullOrEmpty(user.Email);

            if (email == String.Empty)
            {
                if (userHasEmail)
                {
                    user.DeleteEmail();
                }
            }
            else
            {
                if (String.Equals(user.Email, email, StringComparison.OrdinalIgnoreCase))
                {
                    return OperationResult.Succeed;
                }

                var securityCode = await securityCodesService.Get(code);
                securityCodesService.Delete(securityCode);

                if (
                    securityCode == null ||
                    !String.Equals(securityCode.GetParameterValue(SecurityCodeParameterName.UserName), email, StringComparison.OrdinalIgnoreCase))
                {
                    return OperationResult.Failed(1, "Invalid code.");
                }

                user.PutConfirmedEmail(email);
            }

            context.Update(user);
            return OperationResult.Succeed;
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