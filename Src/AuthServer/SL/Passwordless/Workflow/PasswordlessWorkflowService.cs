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
using Microsoft.AspNetCore.Identity;

namespace AuthGuard.SL.Passwordless.Workflow
{
    public class PasswordlessWorkflowService : IPasswordlessWorkflowService
    {
        readonly ISecurityCodesEntityService securityCodesService;
        readonly IEmailSender emailSender;
        readonly ISmsSender smsSender;
        readonly ApplicationDbContext context;
        readonly UserManager<ApplicationUser> userManager;
        readonly SignInManager<ApplicationUser> signInManager;
        readonly IUsersWorkflowService usersService;
        readonly ISecurityCodesRepository securityCodesRepository;

        public PasswordlessWorkflowService(
            ISecurityCodesEntityService securityCodesService,
            IEmailSender emailSender,
            ISmsSender smsSender,
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, IUsersWorkflowService usersService, ISecurityCodesRepository securityCodesRepository)
        {
            this.securityCodesService = securityCodesService;
            this.emailSender = emailSender;
            this.smsSender = smsSender;
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.usersService = usersService;
            this.securityCodesRepository = securityCodesRepository;
        }

        public async Task<OperationResult> SendLogInLinkAsync(CallbackUrlAndUserNameIm im)
        {
            ApplicationUser user = await usersService.GetUserByEmailOrPhoneAsync(im.UserName);

            if (user == null)
            {
                return OperationResult.Failed(1, "User does not exist.");
            }

            var securityCode = SecurityCode.Generate(SecurityCodeAction.PasswordlessLogIn, SecurityCodeParameterName.UserId, user.Id);
            securityCode.AddParameter(SecurityCodeParameterName.UserName, im.UserName);

            securityCodesService.Insert(securityCode);

            var isEmail = im.UserName.Contains("@");

            var code = securityCode.Code.ToString();
            var callbackUrl = im.CallbackUrl.Replace("{code}", code);

            var parameters = new Dictionary<string, string>
            {
                {"Code", code},
                {"CallbackUrl", callbackUrl}
            };

            if (isEmail)
            {
                var email = await emailSender.SendEmailAsync(im.UserName, Template.PasswordlessLogIn, parameters);
                context.Set<Email>().Add(email);
            }
            else
            {
                var sms = await smsSender.SendSmsAsync(ApplicationUser.CleanPhoneNumber(im.UserName), Template.PasswordlessLogIn, parameters);
                context.Set<Sms>().Add(sms);
            }

            await context.SaveChangesAsync();

            return OperationResult.Succeed;
        }

        public async Task<OperationResult> SendSignUpLinkAsync(CallbackUrlAndUserNameIm im)
        {
            var securityCode = SecurityCode.Generate(SecurityCodeAction.PasswordlessSignUp, SecurityCodeParameterName.UserName, im.UserName);
            securityCodesService.Insert(securityCode);

            var isEmail = im.UserName.Contains("@");

            var code = securityCode.Code.ToString();
            var callbackUrl = im.CallbackUrl.Replace("{code}", code);

            var parameters = new Dictionary<string, string>
            {
                {"Code", code},
                {"CallbackUrl", callbackUrl}
            };

            if (isEmail)
            {
                var email = await emailSender.SendEmailAsync(im.UserName, Template.PasswordlessSignUp, parameters);
                context.Set<Email>().Add(email);
            }
            else
            {
                var sms = await smsSender.SendSmsAsync(ApplicationUser.CleanPhoneNumber(im.UserName), Template.PasswordlessSignUp, parameters);
                context.Set<Sms>().Add(sms);
            }

            await context.SaveChangesAsync();

            return OperationResult.Succeed;
        }

        public async Task<OperationResult> SignUpAsync(CodeIm im)
        {
            var securityCode = await securityCodesRepository.ReadByCodeAsync(im.Code);

            if (securityCode == null || securityCode.SecurityCodeAction != SecurityCodeAction.PasswordlessSignUp)
            {
                return OperationResult.Failed(1, "Invalid code.");
            }

            //if (securityCode.ExpiredAt > DateTime.UtcNow)
            //{
            //    securityCodesService.Delete(securityCode);
            //    await context.SaveChangesAsync();
            //    BadRequest(OperationResult.FailedResult(2, "Code is expired.").Errors);
            //}

            securityCodesService.Delete(securityCode);
            var userName = securityCode.GetParameterValue(SecurityCodeParameterName.UserName);

            ApplicationUser user = await usersService.GetUserByEmailOrPhoneAsync(userName);

            if (user == null)
            {
                var isEmail = userName.Contains("@");

                if (isEmail)
                {
                    user = new ApplicationUser
                    {
                        UserName = userName,
                        Email = userName.ToLower(),
                        EmailConfirmed = true
                    };
                }
                else
                {
                    user = new ApplicationUser
                    {
                        UserName = userName,
                        PhoneNumber = userName,
                        PhoneNumberConfirmed = true
                    };
                }

                var result = await userManager.CreateAsync(user);
                if (!result.Succeeded)
                {
                    return OperationResult.Failed(2, result.Errors.First().Description);
                }
            }

            await signInManager.SignInAsync(user, isPersistent: false);
            await context.SaveChangesAsync();

            return OperationResult.Succeed;
        }

        public async Task<OperationResult> LogInAsync(CodeIm im)
        {
            var securityCode = await securityCodesRepository.ReadByCodeAsync(im.Code);

            if (securityCode == null || securityCode.SecurityCodeAction != SecurityCodeAction.PasswordlessLogIn)
            {
                return OperationResult.Failed(1, "Invalid code.");
            }

            //if (securityCode.ExpiredAt > DateTime.UtcNow)
            //{
            //    securityCodesService.Delete(securityCode);
            //    await context.SaveChangesAsync();
            //    BadRequest(OperationResult.FailedResult(2, "Code is expired.").Errors);
            //}

            securityCodesService.Delete(securityCode);

            var user = await userManager.FindByIdAsync(securityCode.GetParameterValue(SecurityCodeParameterName.UserId));
            var userName = securityCode.GetParameterValue(SecurityCodeParameterName.UserName);
            var isEmail = userName.Contains("@");

            if (isEmail)
            {
                if (!user.EmailConfirmed)
                {
                    user.EmailConfirmed = true;
                    await userManager.UpdateAsync(user);
                }
            }
            else
            {
                if (!user.PhoneNumberConfirmed)
                {
                    user.PhoneNumberConfirmed = true;
                    await userManager.UpdateAsync(user);
                }
            }

            await signInManager.SignInAsync(user, isPersistent: false);
            await context.SaveChangesAsync();

            return OperationResult.Succeed;
        }
    }
}