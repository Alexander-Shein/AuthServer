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

        public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ISmsSender smsSender,
            ILoggerFactory loggerFactory,
            IIdentityServerInteractionService interaction,
            IHttpContextAccessor httpContext,
            IClientStore clientStore)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _logger = loggerFactory.CreateLogger<AppsController>();
            _interaction = interaction;
            _clientStore = clientStore;

            _account = new AccountService(interaction, httpContext, clientStore);
        }

        [HttpPost]
        [Route("log-in")]
        public async Task<IActionResult> LogIn([FromBody] LogInIm im)
        {
            if (String.IsNullOrWhiteSpace(im?.UserName) || String.IsNullOrWhiteSpace(im?.Password))
            {
                return BadRequest(new BadRequestResult("Invalid login attempt."));
            }

            var result = await _signInManager.PasswordSignInAsync(im.UserName, im.Password, im.RememberLogIn, lockoutOnFailure: true);

            if (result.IsLockedOut)
            {
                return BadRequest(new BadRequestResult("User account locked out."));
            }

            if (result.Succeeded || result.RequiresTwoFactor)
            {
                return Ok(new LogInVm
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
        public IActionResult ExternalLogIn([FromBody] ExternalLogInIm im)
        {
            var redirectUrl = Url.Action("external-log-in-callback", new
            {
                ReturnUrl = im.ReturnUrl
            });

            var properties = _signInManager.ConfigureExternalAuthenticationProperties(im.AuthenticationScheme, redirectUrl);
            return Challenge(properties, im.AuthenticationScheme);
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
    }

    public class LogInIm
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberLogIn { get; set; }
    }

    public class ExternalLogInIm
    {
        public string ReturnUrl { get; set; }
        public string AuthenticationScheme { get; set; }
    }

    public class LogInVm
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
}
