using IdentityServerWithAspNetIdentity.Models;
using IdentityServerWithAspNetIdentity.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
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
        public async Task<IActionResult> ForgotPasswordAsync(ForgotPasswordIm im)
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

            var code = await userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
            await _emailSender.SendEmailAsync(im.UserName, "Reset Password",
               $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>");

            // If we got this far, something failed, redisplay form
            return Ok();
        }

        [HttpPost]
        [Route("reset")]
        public async Task<IActionResult> ResetPasswordAsync(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction(nameof(AccountController.ResetPasswordConfirmation), "Account");
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(AccountController.ResetPasswordConfirmation), "Account");
            }
            AddErrors(result);
            return View();
        }

        [HttpPut]
        [Route("change")]
        public async Task<IActionResult> ChangePassword(ChangePasswordIm im)
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
        public async Task<IActionResult> AddPassword(PasswordIm im)
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

    public class ForgotPasswordIm
    {
        public string UserName { get; set; }
    }

    public class PasswordIm
    {
        public string Password { get; set; }
    }

    public class ChangePasswordIm : PasswordIm
    {
        public string OldPassword { get; set; }
    }
}