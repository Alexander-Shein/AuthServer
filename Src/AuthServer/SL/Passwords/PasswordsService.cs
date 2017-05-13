using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthGuard.BLL.Domain.Entities;
using AuthGuard.Data;
using AuthGuard.DAL.Repositories.Security;
using AuthGuard.SL.Notifications;
using AuthGuard.SL.Passwordless.Models;
using AuthGuard.SL.Security;
using AuthGuard.SL.Users;
using DddCore.Contracts.BLL.Errors;
using DddCore.Contracts.Crosscutting.UserContext;
using Microsoft.AspNetCore.Identity;

namespace AuthGuard.SL.Passwords
{
    public class PasswordsService : IPasswordsService
    {
        readonly IUsersWorkflowService usersService;
        readonly ISecurityCodesEntityService securityCodesService;
        readonly IEmailSender emailSender;
        readonly ISmsSender smsSender;
        readonly ApplicationDbContext context;
        readonly UserManager<ApplicationUser> userManager;
        readonly IUserContext<Guid> userContext;
        readonly SignInManager<ApplicationUser> signInManager;
        readonly ISecurityCodesRepository securityCodesRepository;

        public PasswordsService(IUsersWorkflowService usersService, ISecurityCodesEntityService securityCodesService, IEmailSender emailSender, ISmsSender smsSender, ApplicationDbContext context, UserManager<ApplicationUser> userManager, IUserContext<Guid> userContext, SignInManager<ApplicationUser> signInManager, ISecurityCodesRepository securityCodesRepository)
        {
            this.usersService = usersService;
            this.securityCodesService = securityCodesService;
            this.emailSender = emailSender;
            this.smsSender = smsSender;
            this.context = context;
            this.userManager = userManager;
            this.userContext = userContext;
            this.signInManager = signInManager;
            this.securityCodesRepository = securityCodesRepository;
        }

        public async Task<OperationResult> ForgotPasswordAsync(CallbackUrlAndUserNameIm im)
        {
            if (String.IsNullOrWhiteSpace(im.UserName))
            {
                return OperationResult.Failed(1, "Invalid user name.");
            }

            var user = await usersService.GetUserByEmailOrPhoneAsync(im.UserName);

            if (user == null)
            {
                return OperationResult.Failed(2, $"User with '{im.UserName}' doesn't exist.");
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

            return OperationResult.Succeed;
        }

        public async Task<OperationResult> ResetPasswordAsync(ResetPasswordIm im)
        {
            var securityCode = await securityCodesRepository.ReadByCodeAsync(im.Code);

            if (securityCode == null || securityCode.SecurityCodeAction != SecurityCodeAction.ResetPassword)
            {
                return OperationResult.Failed(1, "Invalid code.");
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

            if (!result.Succeeded)
            {
                return OperationResult.Failed(2, result.Errors.First().Description);
            }

            securityCodesService.Delete(securityCode);

            await context.SaveChangesAsync();

            return OperationResult.Succeed;
        }

        public async Task<OperationResult> ChangePasswordAsync(ChangePasswordIm im)
        {
            var user = await userManager.FindByIdAsync(userContext.Id.ToString());

            var result = await userManager.ChangePasswordAsync(user, im.OldPassword, im.Password);
            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, isPersistent: false);
                return OperationResult.Succeed;
            }

            return OperationResult.Failed(1, result.Errors.First().Description);
        }

        public async Task<OperationResult> AddPassword(PasswordIm im)
        {
            var user = await userManager.FindByIdAsync(userContext.Id.ToString());

            var result = await userManager.AddPasswordAsync(user, im.Password);
            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, isPersistent: false);
                return OperationResult.Succeed;
            }

            return OperationResult.Failed(1, result.Errors.First().Description);
        }
    }
}