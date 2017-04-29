using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuthGuard.BLL.Domain.Entities;
using AuthGuard.Data;
using AuthGuard.Services;
using AuthGuard.Services.Security;
using AuthGuard.Services.Users;
using DddCore.Contracts.BLL.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthGuard.Api
{
    [EnableCors("default")]
    [Route("api/[controller]")]
    public class PasswordsController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IEmailSender emailSender;
        private readonly ISmsSender smsSender;
        private readonly IUsersService usersService;
        readonly ApplicationDbContext context;
        readonly ISecurityCodesService securityCodesService;

        public PasswordsController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ISmsSender smsSender,
            IUsersService usersService,
            ApplicationDbContext context,
            ISecurityCodesService securityCodesService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailSender = emailSender;
            this.smsSender = smsSender;
            this.usersService = usersService;
            this.context = context;
            this.securityCodesService = securityCodesService;
        }

        [HttpPost]
        [Route("forgot")]
        public async Task<IActionResult> ForgotPasswordAsync([FromBody] ForgotPasswordIm im)
        {
            if (String.IsNullOrWhiteSpace(im.UserName))
            {
                return BadRequest(new List<Error>
                {
                    new Error {Code = 1, Description = "Invalid user name."}
                });
            }

            var user = await usersService.GetUserByEmailOrPhoneAsync(im.UserName);

            if (user == null)
            {
                return BadRequest(new List<Error>
                {
                    new Error {Code = 2, Description = $"User with '{im.UserName}' doesn't exist."}
                });
            }

            var isEmail = im.UserName.Contains("@");

            var securityCode = SecurityCode.Generate(SecurityCodeAction.ResetPassword, user.Id);
            securityCodesService.Insert(securityCode);

            var code = securityCode.Code.ToString();

            var callbakUrl = im.ResetPasswordUrl.Replace("{code}", code);// += $";code={WebUtility.UrlEncode(code)};userName={WebUtility.UrlEncode(user.UserName)}";

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
            return Ok();
        }

        [HttpPost]
        [Route("reset")]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordIm im)
        {
            var securityCode = await securityCodesService.Get(im.Code);

            if (securityCode == null || securityCode.SecurityCodeAction != SecurityCodeAction.ResetPassword)
            {
                return BadRequest(OperationResult.FailedResult(1, "Invalid code.").Errors);
            }

            //if (securityCode.ExpiredAt > DateTime.UtcNow)
            //{
            //    securityCodesService.Delete(securityCode);
            //    await context.SaveChangesAsync();
            //    BadRequest(OperationResult.FailedResult(2, "Code is expired.").Errors);
            //}

            var user = await userManager.FindByIdAsync(securityCode.UserId);
            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var result = await userManager.ResetPasswordAsync(user, token, im.Password);

            securityCodesService.Delete(securityCode);

            return Ok();
        }

        [HttpPut]
        [Authorize]
        [Route("change")]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordIm im)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            var result = await userManager.ChangePasswordAsync(user, im.OldPassword, im.Password);
            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, isPersistent: false);
                return Ok();
            }

            return BadRequest(result.Errors);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddPassword([FromBody] PasswordIm im)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            var result = await userManager.AddPasswordAsync(user, im.Password);
            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, isPersistent: false);
                return Ok();
            }

            return BadRequest(result.Errors);
        }
    }

    public class ForgotPasswordIm : UserNameIm
    {
        public string ResetPasswordUrl { get; set; }
    }

    public class PasswordIm
    {
        public string Password { get; set; }
    }

    public class ChangePasswordIm : PasswordIm
    {
        public string OldPassword { get; set; }
    }

    public class ResetPasswordIm
    {
        public string Password { get; set; }
        public int Code { get; set; }
    }
}