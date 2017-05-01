using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthGuard.BLL.Domain.Entities;
using AuthGuard.Data;
using AuthGuard.Services.Passwordless;
using AuthGuard.Services.Security;
using AuthGuard.Services.Users;
using DddCore.Contracts.BLL.Errors;
using DddCore.Contracts.Crosscutting.UserContext;
using Microsoft.AspNetCore.Identity;

namespace AuthGuard.Services.Passwords
{
    public class PasswordsService : IPasswordsService
    {
        readonly IUsersService usersService;
        readonly ISecurityCodesService securityCodesService;
        readonly IEmailSender emailSender;
        readonly ISmsSender smsSender;
        readonly ApplicationDbContext context;
        readonly UserManager<ApplicationUser> userManager;
        readonly IUserContext<Guid> userContext;
        readonly SignInManager<ApplicationUser> signInManager;

        public PasswordsService(IUsersService usersService, ISecurityCodesService securityCodesService, IEmailSender emailSender, ISmsSender smsSender, ApplicationDbContext context, UserManager<ApplicationUser> userManager, IUserContext<Guid> userContext, SignInManager<ApplicationUser> signInManager)
        {
            this.usersService = usersService;
            this.securityCodesService = securityCodesService;
            this.emailSender = emailSender;
            this.smsSender = smsSender;
            this.context = context;
            this.userManager = userManager;
            this.userContext = userContext;
            this.signInManager = signInManager;
        }

        public async Task<OperationResult> ForgotPasswordAsync(CallbackUrlAndUserNameIm im)
        {
            if (String.IsNullOrWhiteSpace(im.UserName))
            {
                return OperationResult.FailedResult(1, "Invalid user name.");
            }

            var user = await usersService.GetUserByEmailOrPhoneAsync(im.UserName);

            if (user == null)
            {
                return OperationResult.FailedResult(2, $"User with '{im.UserName}' doesn't exist.");
            }

            var isEmail = im.UserName.Contains("@");

            var securityCode = SecurityCode.Generate(SecurityCodeAction.ResetPassword, SecurityCodeParameterName.UserId, user.Id);
            securityCodesService.Insert(securityCode);

            var code = securityCode.Code.ToString();

            var callbakUrl = im.CallbackUrl.Replace("{code}", code);

            var parameters = new Dictionary<string, string>
            {
                {"Code", code},
                {"CallbackUrl", callbakUrl}
            };

            if (isEmail)
            {
                var email = await emailSender.SendEmailAsync(user.Email, Template.ResetPassword, parameters);
                context.Set<Email>().Add(email);
            }
            else
            {
                var sms = await smsSender.SendSmsAsync(user.PhoneNumber, Template.ResetPassword, parameters);
                context.Set<Sms>().Add(sms);
            }

            await context.SaveChangesAsync();

            return OperationResult.SucceedResult;
        }

        public async Task<OperationResult> ResetPasswordAsync(ResetPasswordIm im)
        {
            var securityCode = await securityCodesService.Get(im.Code);

            if (securityCode == null || securityCode.SecurityCodeAction != SecurityCodeAction.ResetPassword)
            {
                return OperationResult.FailedResult(1, "Invalid code.");
            }

            //if (securityCode.ExpiredAt > DateTime.UtcNow)
            //{
            //    securityCodesService.Delete(securityCode);
            //    await context.SaveChangesAsync();
            //    BadRequest(OperationResult.FailedResult(2, "Code is expired.").Errors);
            //}

            var user = await userManager.FindByIdAsync(securityCode.GetParameterValue(SecurityCodeParameterName.UserId));
            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var result = await userManager.ResetPasswordAsync(user, token, im.Password);

            securityCodesService.Delete(securityCode);

            return OperationResult.SucceedResult;
        }

        public async Task<OperationResult> ChangePasswordAsync(ChangePasswordIm im)
        {
            var user = await userManager.FindByIdAsync(userContext.Id.ToString());

            var result = await userManager.ChangePasswordAsync(user, im.OldPassword, im.Password);
            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, isPersistent: false);
                return OperationResult.SucceedResult;
            }

            return OperationResult.FailedResult(1, Enumerable.First<IdentityError>(result.Errors).Description);
        }

        public async Task<OperationResult> AddPassword(PasswordIm im)
        {
            var user = await userManager.FindByIdAsync(userContext.Id.ToString());

            var result = await userManager.AddPasswordAsync(user, im.Password);
            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, isPersistent: false);
                return OperationResult.SucceedResult;
            }

            return OperationResult.FailedResult(1, Enumerable.First<IdentityError>(result.Errors).Description);
        }
    }
}