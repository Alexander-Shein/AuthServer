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
        public async Task<IActionResult> LogIn([FromBody] LogInDetails im)
        {
            if (String.IsNullOrWhiteSpace(im?.Email) || String.IsNullOrWhiteSpace(im?.Password))
            {
                return BadRequest(new ErrorResult("Invalid login attempt."));
            }

            var result = await _signInManager.PasswordSignInAsync(im.Email ?? im.Phone, im.Password, im.RememberLogIn, lockoutOnFailure: true);

            if (result.IsLockedOut)
            {
                return BadRequest(new ErrorResult("User account locked out."));
            }

            if (result.Succeeded || result.RequiresTwoFactor)
            {
                return Ok(new LogInResult
                {
                    RequiresTwoFactor = result.RequiresTwoFactor
                });
            }
            else
            {
                return BadRequest(new ErrorResult("Invalid login attempt."));
            }
        }
    }

    public class LogInDetails
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public bool RememberLogIn { get; set; }
    }

    public class LogInResult
    {
        public bool RequiresTwoFactor { get; set; }
    }

    public class ErrorResult
    {
        public ErrorResult() { }

        public ErrorResult(string message)
        {
            Error = message;
        }

        public string Error { get; set; }
    }
}
