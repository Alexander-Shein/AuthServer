using IdentityServerWithAspNetIdentity.Models;
using IdentityServerWithAspNetIdentity.Models.AccountViewModels;
using IdentityServerWithAspNetIdentity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AuthServer.Api
{
    [EnableCors("default")]
    [Route("api/[controller]")]
    public class PasswordsController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly IUsersService usersService;

        public PasswordsController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ISmsSender smsSender,
            IUsersService usersService)
        {
            this.userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _smsSender = smsSender;
            this.usersService = usersService;
        }

        [HttpPost]
        [Route("forgot")]
        public async Task<IActionResult> ForgotPasswordAsync([FromBody] ForgotPasswordIm im)
        {
            if (String.IsNullOrWhiteSpace(im.UserName))
            {
                return BadRequest(new BadRequestResult($"Invalid user name."));
            }

            var user = await usersService.GetUserByEmailOrPhoneAsync(im.UserName);

            if (user == null)
            {
                return BadRequest(new BadRequestResult($"User with '{im.UserName}' doesn't exist."));
            }

            var isEmail = im.UserName.Contains("@");
            var code = await userManager.GeneratePasswordResetTokenAsync(user);
            im.ResetPasswordUrl += $";code={code};userName={WebUtility.UrlEncode(user.UserName)}";

            if (isEmail)
            {
                await
                    _emailSender
                        .SendEmailAsync(
                            user.Email,
                            "Reset Password",
                            $"Please reset your password by clicking here: <a href='{im.ResetPasswordUrl}'>link</a>");
            }
            else
            {
                await _smsSender.SendSmsAsync(user.PhoneNumber, $"Code: {code}. Please use this code to reset your password.");
            }

            return Ok();
        }

        [HttpPost]
        [Route("reset")]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordIm im)
        {
            ApplicationUser user = await usersService.GetUserByEmailOrPhoneAsync(im.UserName);

            if (user == null)
            {
                return BadRequest(new BadRequestResult($"User with '{im.UserName}' doesn't exist."));
            }

            var result = await userManager.ResetPasswordAsync(user, im.Code, im.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return Ok();
            }

            return BadRequest(new BadRequestResult(result.Errors.First().Description));
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
                await _signInManager.SignInAsync(user, isPersistent: false);
                return Ok();
            }
            else
            {
                return BadRequest(new BadRequestResult(result.Errors.First().Description));
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddPassword([FromBody] PasswordIm im)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            var result = await userManager.AddPasswordAsync(user, im.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return Ok();
            }
            else
            {
                return BadRequest(new BadRequestResult(result.Errors.First().Description));
            }
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