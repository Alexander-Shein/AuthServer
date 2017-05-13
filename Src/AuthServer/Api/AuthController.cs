using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthGuard.BLL.Domain.Entities;
using AuthGuard.Data;
using AuthGuard.SL;
using AuthGuard.SL.Notifications;
using AuthGuard.SL.Security;
using AuthGuard.SL.Users;
using DddCore.Contracts.BLL.Errors;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AuthGuard.Api
{
    [EnableCors("default")]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        readonly UserManager<ApplicationUser> userManager;
        readonly SignInManager<ApplicationUser> signInManager;
        readonly IEmailSender emailSender;
        readonly ISmsSender smsSender;
        readonly ILogger logger;
        readonly IUsersWorkflowService usersService;
        readonly ApplicationDbContext context;
        readonly ISecurityCodesEntityService securityCodesService;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ISmsSender smsSender,
            ILoggerFactory loggerFactory,
            IIdentityServerInteractionService interaction,
            IHttpContextAccessor httpContext,
            IClientStore clientStore,
            IUsersWorkflowService usersService,
            ApplicationDbContext context,
            ISecurityCodesEntityService securityCodesService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailSender = emailSender;
            this.smsSender = smsSender;
            logger = loggerFactory.CreateLogger<AppsController>();
            this.usersService = usersService;
            this.context = context;
            this.securityCodesService = securityCodesService;
        }

        [HttpPost]
        [Route("sign-up")]
        public async Task<IActionResult> SignUpAsync([FromBody] SignUpIm im)
        {
            if (String.IsNullOrWhiteSpace(im?.UserName) || String.IsNullOrWhiteSpace(im.Password))
            {
                return BadRequest(new List<Error>
                {
                    new Error {Code = 1, Description = "Invalid username or password."}
                });
            }

            var app = await context.Set<App>().FindAsync(im.AppId);

            if (app == null)
            {
                return BadRequest(new List<Error>
                {
                    new Error {Code = 2, Description = $"App with key: '{im.AppId}' does not exist."}
                });
            }

            ApplicationUser user;
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
                var phone = ApplicationUser.CleanPhoneNumber(im.UserName);

                user = new ApplicationUser
                {
                    UserName = phone,
                    PhoneNumber = phone
                };
            }

            var result = await userManager.CreateAsync(user, im.Password);
            if (result.Succeeded)
            {
                var securityCode = SecurityCode.Generate(SecurityCodeAction.ConfirmAccount, SecurityCodeParameterName.UserId, user.Id);

                var code = securityCode.Code.ToString();
                var callbackUrl = im.AccountConfirmationUrl.Replace("{code}", code);

                var parameters = new Dictionary<string, string>
                {
                    {"Code", code},
                    {"CallbackUrl", callbackUrl}
                };

                if (isEmail)
                {
                    securityCode.AddParameter(SecurityCodeParameterName.LocalProvider, LocalProvider.Email.ToString());
                    var email = await emailSender.SendEmailAsync(user.Email, Template.AccountConfirmation, parameters);
                    context.Set<Email>().Add(email);
                }
                else
                {
                    securityCode.AddParameter(SecurityCodeParameterName.LocalProvider, LocalProvider.Phone.ToString());
                    var sms = await smsSender.SendSmsAsync(user.PhoneNumber, Template.AccountConfirmation, parameters);
                    context.Set<Sms>().Add(sms);
                }

                securityCodesService.Insert(securityCode);

                var signUpResult = new SignUpResultVm();

                if (isEmail && !app.IsEmailConfirmationRequired || !isEmail && !app.IsPhoneConfirmationRequired)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                }
                else
                {
                    signUpResult.IsConfirmationRequired = true;
                }

                logger.LogInformation(3, "User created a new account with password.");

                app.UsersCount++;
                context.Set<App>().Update(app);

                await context.SaveChangesAsync();
                return Ok(signUpResult);
            }

            return BadRequest(result.Errors);
        }

        [HttpPost]
        [Route("log-in")]
        public async Task<IActionResult> LogInAsync([FromBody] LogInIm im)
        {
            if (String.IsNullOrWhiteSpace(im?.UserName) || String.IsNullOrWhiteSpace(im.Password))
            {
                return BadRequest(new List<Error>
                {
                    new Error {Code = 1, Description = "Invalid log in attempt."}
                });
            }

            var user = await usersService.GetUserByEmailOrPhoneAsync(im.UserName);
            if (user == null)
            {
                return BadRequest(new List<Error>
                {
                    new Error {Code = 1, Description = "Invalid log in attempt."}
                });
            }

            var result = await signInManager.PasswordSignInAsync(user, im.Password, im.RememberLogIn, lockoutOnFailure: true);

            if (result.IsLockedOut)
            {
                return BadRequest(new List<Error>
                {
                    new Error {Code = 2, Description = "User account locked out."}
                });
            }

            if (result.Succeeded || result.RequiresTwoFactor)
            {
                return Ok(new LogInResultVm
                {
                    RequiresTwoFactor = result.RequiresTwoFactor
                });
            }

            return BadRequest(new List<Error>
            {
                new Error {Code = 1, Description = "Invalid log in attempt."}
            });
        }

        [HttpPost]
        [Route("external-log-in")]
        public IActionResult ExternalLogIn(string authenticationScheme, string returnUrl)
        {
            var redirectUrl = $"{Request.PathBase}/api/auth/external-log-in-callback?returnUrl={WebUtility.UrlEncode(returnUrl)}";

            var properties = signInManager.ConfigureExternalAuthenticationProperties(authenticationScheme, redirectUrl);
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
            var info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                returnUrl += returnUrl.Contains("?") ? "&" : "?";
                returnUrl += "errorMessage=You are not logged in. Please try again.";
                return Redirect(returnUrl);
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);

            if (result.Succeeded)
            {
                logger.LogInformation(5, "User logged in with {Name} provider.", info.LoginProvider);
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
            var user = await userManager.GetUserAsync(User);

            var result = await userManager.RemoveLoginAsync(user, authenticationScheme, key);
            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, isPersistent: false);
                return Ok();
            }

            return BadRequest(result.Errors);
        }

        [Authorize]
        [HttpPost]
        [Route("external-provider")]
        public IActionResult LinkExternalLogIn(string provider, string returnUrl)
        {
            var redirectUrl = $"{Request.PathBase}/api/auth/external-provider-callback?returnUrl={WebUtility.UrlEncode(returnUrl)}";
            var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl, userManager.GetUserId(User));
            return Challenge(properties, provider);
        }

        [Authorize]
        [HttpGet]
        [Route("external-provider-callback")]
        public async Task<ActionResult> LinkExternalLogInCallback(string returnUrl)
        {
            var user = await userManager.GetUserAsync(User);
            var info = await signInManager.GetExternalLoginInfoAsync(await userManager.GetUserIdAsync(user));

            if (info == null)
            {
                returnUrl += returnUrl.Contains("?") ? "&" : "?";
                returnUrl += "errorMessage=Failed";
                return Redirect(returnUrl);
            }
            var result = await userManager.AddLoginAsync(user, info);

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

    public class UserNameIm
    {
        public string UserName { get; set; }
    }

    public class LogInIm  : UserNameIm
    {
        public string Password { get; set; }
        public bool RememberLogIn { get; set; }
    }

    public class SignUpIm
    {
        public Guid AppId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string AccountConfirmationUrl { get; set; }
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

    public class SendCodeIm
    {
        public string Provider { get; set; }
    }
}
