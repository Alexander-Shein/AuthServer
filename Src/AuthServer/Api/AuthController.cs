using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using IdentityServerWithAspNetIdentity.Models;
using IdentityServerWithAspNetIdentity.Services;
using Microsoft.Extensions.Logging;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Quickstart.UI;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using System;
using System.Security.Claims;
using System.Net;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthServer.Api
{
    [EnableCors("default")]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly AccountService _account;
        private readonly IUsersService usersService;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ISmsSender smsSender,
            ILoggerFactory loggerFactory,
            IIdentityServerInteractionService interaction,
            IHttpContextAccessor httpContext,
            IClientStore clientStore,
            IUsersService usersService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _logger = loggerFactory.CreateLogger<AppsController>();
            _interaction = interaction;
            _clientStore = clientStore;
            this.usersService = usersService;

            _account = new AccountService(interaction, httpContext, clientStore);
        }

        [HttpPost]
        [Route("sign-up")]
        public async Task<IActionResult> SignUpAsunc([FromBody] SignUpIm im)
        {
            if (String.IsNullOrWhiteSpace(im?.UserName) || String.IsNullOrWhiteSpace(im?.Password))
            {
                return BadRequest(new BadRequestResult("Invalid username or password."));
            }

            ApplicationUser user;
            var isEmail = im.UserName.Contains("@");

            if (isEmail)
            {
                user = new ApplicationUser
                {
                    UserName = im.UserName,
                    Email = im.UserName
                };
            }
            else
            {
                var phone = usersService.CleanPhoneNumber(im.UserName);

                user = new ApplicationUser
                {
                    UserName = phone,
                    PhoneNumber = phone
                };
            }

            var result = await _userManager.CreateAsync(user, im.Password);
            if (result.Succeeded)
            {
                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
                // Send an email with this link
                //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                //await _emailSender.SendEmailAsync(model.Email, "Confirm your account",
                //    $"Please confirm your account by clicking this link: <a href='{callbackUrl}'>link</a>");
                await _signInManager.SignInAsync(user, isPersistent: false);
                _logger.LogInformation(3, "User created a new account with password.");
                return Ok();
            }

            return BadRequest(new BadRequestResult(result.Errors.First().Description));
        }

        [HttpPost]
        [Route("log-in")]
        public async Task<IActionResult> LogInAsync([FromBody] LogInIm im)
        {
            if (String.IsNullOrWhiteSpace(im?.UserName) || String.IsNullOrWhiteSpace(im?.Password))
            {
                return BadRequest(new BadRequestResult("Invalid log in attempt."));
            }

            var user = await usersService.GetUserByEmailOrPhoneAsync(im.UserName);
            if (user == null) return BadRequest(new BadRequestResult("Invalid login attempt."));

            var result = await _signInManager.PasswordSignInAsync(user, im.Password, im.RememberLogIn, lockoutOnFailure: true);

            if (result.IsLockedOut)
            {
                return BadRequest(new BadRequestResult("User account locked out."));
            }

            if (result.Succeeded || result.RequiresTwoFactor)
            {
                return Ok(new LogInResultVm
                {
                    RequiresTwoFactor = result.RequiresTwoFactor
                });
            }
            else
            {
                return BadRequest(new BadRequestResult("Invalid login attempt."));
            }
        }

        [HttpPost]
        [Route("external-log-in")]
        public IActionResult ExternalLogIn(string authenticationScheme, string returnUrl)
        {
            var redirectUrl = $"http://localhost:5000/api/auth/external-log-in-callback?returnUrl={WebUtility.UrlEncode(returnUrl)}";

            var properties = _signInManager.ConfigureExternalAuthenticationProperties(authenticationScheme, redirectUrl);
            return Challenge(properties, authenticationScheme);
        }

        [HttpGet]
        [Route("external-log-in-callback")]
        public async Task<IActionResult> ExternalLogInCallback(string returnUrl, string remoteError = null)
        {
            if (remoteError != null)
            {
                returnUrl += returnUrl.Contains("?") ? "&" : "?";
                returnUrl += "errorMessage=" + remoteError;
                return Redirect(returnUrl);
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                returnUrl += returnUrl.Contains("?") ? "&" : "?";
                returnUrl += "errorMessage=You are not logged in. Please try again.";
                return Redirect(returnUrl);
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);

            if (result.Succeeded)
            {
                _logger.LogInformation(5, "User logged in with {Name} provider.", info.LoginProvider);
                return Redirect(returnUrl);
            }
            if (result.RequiresTwoFactor)
            {
                returnUrl += returnUrl.Contains("?") ? "&" : "?";
                returnUrl += "requiresTwoFactor=true";
                return Redirect(returnUrl);
            }
            if (result.IsLockedOut)
            {
                returnUrl += returnUrl.Contains("?") ? "&" : "?";
                returnUrl += "errorMessage=Account is lockout.";
                return Redirect(returnUrl);
            }
            else
            {
                returnUrl += returnUrl.Contains("?") ? "&" : "?";
                returnUrl += "isNewAccount=true";
                returnUrl += "&provider=" + info.LoginProvider;

                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                if (!String.IsNullOrEmpty(email))
                {
                    returnUrl += "&email=" + email;
                }

                var mobilePhone = info.Principal.FindFirstValue(ClaimTypes.MobilePhone);
                if (!String.IsNullOrEmpty(mobilePhone))
                {
                    returnUrl += "&phone=" + mobilePhone;
                }

                return Redirect(returnUrl);
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("external-provider")]
        public async Task<IActionResult> DeleteExternalLogIn(string authenticationScheme, string key)
        {
            var user = await _userManager.GetUserAsync(User);

            var result = await _userManager.RemoveLoginAsync(user, authenticationScheme, key);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return Ok();
            }

            return BadRequest(new BadRequestResult(result.Errors.First().Description));
        }

        [Authorize]
        [HttpPost]
        [Route("external-provider")]
        public IActionResult LinkExternalLogIn(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider to link a login for the current user
            var redirectUrl = $"http://localhost:5000/api/auth/external-provider-callback?returnUrl={WebUtility.UrlEncode(returnUrl)}";
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl, _userManager.GetUserId(User));
            return Challenge(properties, provider);
        }

        [Authorize]
        [HttpGet]
        [Route("external-provider-callback")]
        public async Task<ActionResult> LinkExternalLogInCallback(string returnUrl)
        {
            var user = await _userManager.GetUserAsync(User);
            var info = await _signInManager.GetExternalLoginInfoAsync(await _userManager.GetUserIdAsync(user));

            if (info == null)
            {
                returnUrl += returnUrl.Contains("?") ? "&" : "?";
                returnUrl += "errorMessage=Failed";
                return Redirect(returnUrl);
            }
            var result = await _userManager.AddLoginAsync(user, info);

            if (result.Succeeded)
            {
                return Redirect(returnUrl);
            }
            else
            {
                returnUrl += returnUrl.Contains("?") ? "&" : "?";
                returnUrl += "errorMessage=" + result.Errors.First().Description;
                return Redirect(returnUrl);
            }
        }
    }

    public class LogInIm
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberLogIn { get; set; }
    }

    public class SignUpIm
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class SignUpResultVm
    {
        public bool IsConfirmationRequired { get; set; }
    }

    public class ExternalLogInIm
    {
        public string ReturnUrl { get; set; }
        public string AuthenticationScheme { get; set; }
    }

    public class LogInResultVm
    {
        public bool RequiresTwoFactor { get; set; }
    }

    public class BadRequestResult
    {
        public BadRequestResult() { }

        public BadRequestResult(string message)
        {
            Error = message;
        }

        public string Error { get; set; }
    }

    public class SendCodeIm
    {
        public string Provider { get; set; }
    }
}
