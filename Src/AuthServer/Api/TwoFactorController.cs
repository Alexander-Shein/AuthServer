using IdentityServerWithAspNetIdentity.Models;
using IdentityServerWithAspNetIdentity.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Api
{
    [EnableCors("default")]
    [Route("api/two-factor")]
    public class TwoFactorController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;

        public TwoFactorController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ISmsSender smsSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _smsSender = smsSender;
        }

        [HttpGet]
        [Route("providers")]
        public async Task<IActionResult> GetTwoFactorProviders()
        {
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                return BadRequest("Have no user for 2 factor verification.");
            }
            var userFactors = await _userManager.GetValidTwoFactorProvidersAsync(user);
            var providers = userFactors.Select(x => new Provider
            {
                Name = x,
                Value = x
            });

            return Ok(providers);
        }

        [HttpPost]
        [Route("codes")]
        public async Task<IActionResult> SendCode([FromBody] Provider im)
        {
            var provider = im.Value;

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                return BadRequest("Have no user for 2 factor verification.");
            }

            // Generate the token and send it
            var code = await _userManager.GenerateTwoFactorTokenAsync(user, provider);
            if (string.IsNullOrWhiteSpace(code))
            {
                return BadRequest("An error is occured. Please try again.");
            }

            var parameters = new Dictionary<string, string>
            {
                {"Code", code}
            };

            if (provider == "Email")
            {
                await _emailSender.SendEmailAsync(await _userManager.GetEmailAsync(user), "SecurityCode", parameters);
            }
            else if (provider == "Phone")
            {
                await _smsSender.SendSmsAsync(await _userManager.GetPhoneNumberAsync(user), "SecurityCode", parameters);
            }
            else
            {
                return BadRequest("An error is occured. Please try again.");
            }

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> VerifyCode([FromBody] TwoFactorVerificationIm model)
        {
            var result = await _signInManager.TwoFactorSignInAsync(model.Provider, model.Code, model.RememberLogIn, model.RememberBrowser);
            if (result.Succeeded)
            {
                return Ok();
            }
            if (result.IsLockedOut)
            {
                return BadRequest("User account locked out.");
            }
            else
            {
                return BadRequest("An error is occured. Please try again.");
            }
        }

        [HttpPut]
        [Route("settings")]
        public async Task<IActionResult> UpdateTwoFactorSettings([FromBody] TwoFactorSettings im)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user != null)
            {
                await _userManager.SetTwoFactorEnabledAsync(user, im.Enabled);
                await _signInManager.SignInAsync(user, isPersistent: false);
            }
            return Ok(im);
        }
    }

    public class TwoFactorVerificationIm
    {
        public string Provider { get; set; }
        public string Code { get; set; }
        public bool RememberBrowser { get; set; }
        public bool RememberLogIn { get; set; }
    }

    public class Provider
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class TwoFactorSettings
    {
        public bool Enabled { get; set; }
    }
}