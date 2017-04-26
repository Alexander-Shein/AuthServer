using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AuthGuard.BLL.Domain.Entities;
using AuthGuard.Data;
using AuthGuard.Services;
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

        public PasswordsController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ISmsSender smsSender,
            IUsersService usersService,
            ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailSender = emailSender;
            this.smsSender = smsSender;
            this.usersService = usersService;
            this.context = context;
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
            var code = await userManager.GeneratePasswordResetTokenAsync(user);
            im.ResetPasswordUrl += $";code={WebUtility.UrlEncode(code)};userName={WebUtility.UrlEncode(user.UserName)}";

            var parameters = new Dictionary<string, string>
            {
                {"Code", code},
                {"CallbackUrl", im.ResetPasswordUrl}
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
            ApplicationUser user = await usersService.GetUserByEmailOrPhoneAsync(im.UserName);

            if (user == null)
            {
                return BadRequest(new List<Error>
                {
                    new Error {Code = 1, Description = $"User with '{im.UserName}' doesn't exist."}
                });
            }

            var result = await userManager.ResetPasswordAsync(user, im.Code, im.Password);
            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, false);
                return Ok();
            }

            return BadRequest(result.Errors);
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

    public class ResetPasswordIm : UserNameIm
    {
        public string Password { get; set; }
        public string Code { get; set; }
    }
}