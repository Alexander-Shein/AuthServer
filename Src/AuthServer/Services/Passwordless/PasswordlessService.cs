using System.Collections.Generic;
using System.Threading.Tasks;
using AuthGuard.BLL.Domain.Entities;
using AuthGuard.Data;
using AuthGuard.Services.Security;
using AuthGuard.Services.Users;
using DddCore.Contracts.BLL.Errors;
using Microsoft.AspNetCore.Identity;

namespace AuthGuard.Services.Passwordless
{
    public class PasswordlessService : IPasswordlessService
    {
        readonly ISecurityCodesService securityCodesService;
        readonly IEmailSender emailSender;
        readonly ISmsSender smsSender;
        readonly ApplicationDbContext context;
        readonly UserManager<ApplicationUser> userManager;
        readonly SignInManager<ApplicationUser> signInManager;
        readonly IUsersService usersService;

        public PasswordlessService(
            ISecurityCodesService securityCodesService,
            IEmailSender emailSender,
            ISmsSender smsSender,
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, IUsersService usersService)
        {
            this.securityCodesService = securityCodesService;
            this.emailSender = emailSender;
            this.smsSender = smsSender;
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.usersService = usersService;
        }

        public async Task<OperationResult> SendLogInLinkAsync(CallbackUrlAndUserNameIm im)
        {
            ApplicationUser user = await usersService.GetUserByEmailOrPhoneAsync(im.UserName);

            if (user == null)
            {
                return OperationResult.FailedResult(1, "User does not exist.");
            }

            var securityCode = SecurityCode.Generate(SecurityCodeAction.PasswordlessLogIn, user.Id);
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

            return OperationResult.SucceedResult;
        }

        public async Task<OperationResult> SendSignUpLinkAsync(CallbackUrlAndUserNameIm im)
        {
            var securityCode = SecurityCode.Generate(SecurityCodeAction.PasswordlessSignUp, null);
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

            return OperationResult.SucceedResult;
        }

        public async Task<OperationResult> SignUpAsync(CodeAndUserNameIm im)
        {
            var securityCode = await securityCodesService.Get(im.Code);

            if (securityCode == null || securityCode.SecurityCodeAction != SecurityCodeAction.PasswordlessSignUp)
            {
                return OperationResult.FailedResult(1, "Invalid code.");
            }

            //if (securityCode.ExpiredAt > DateTime.UtcNow)
            //{
            //    securityCodesService.Delete(securityCode);
            //    await context.SaveChangesAsync();
            //    BadRequest(OperationResult.FailedResult(2, "Code is expired.").Errors);
            //}

            securityCodesService.Delete(securityCode);

            ApplicationUser user = await usersService.GetUserByEmailOrPhoneAsync(im.UserName);

            if (user == null)
            {
                var isEmail = im.UserName.Contains("@");

                if (isEmail)
                {
                    user = new ApplicationUser
                    {
                        UserName = im.UserName,
                        Email = im.UserName.ToLower()
                    };
                }
                else
                {
                    user = new ApplicationUser
                    {
                        UserName = im.UserName,
                        PhoneNumber = im.UserName
                    };
                }

                var result = await userManager.CreateAsync(user);
            }

            await signInManager.SignInAsync(user, isPersistent: false);
            await context.SaveChangesAsync();

            return OperationResult.SucceedResult;
        }

        public async Task<OperationResult> LogInAsync(CodeIm im)
        {
            var securityCode = await securityCodesService.Get(im.Code);

            if (securityCode == null || securityCode.SecurityCodeAction != SecurityCodeAction.PasswordlessSignUp)
            {
                return OperationResult.FailedResult(1, "Invalid code.");
            }

            //if (securityCode.ExpiredAt > DateTime.UtcNow)
            //{
            //    securityCodesService.Delete(securityCode);
            //    await context.SaveChangesAsync();
            //    BadRequest(OperationResult.FailedResult(2, "Code is expired.").Errors);
            //}

            securityCodesService.Delete(securityCode);

            var user = await userManager.FindByIdAsync(securityCode.UserId);

            await signInManager.SignInAsync(user, isPersistent: false);
            await context.SaveChangesAsync();

            return OperationResult.SucceedResult;
        }
    }
}