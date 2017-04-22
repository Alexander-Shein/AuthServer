using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthGuard.Data.Entities;
using AuthGuard.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthGuard.Api
{
    [EnableCors("default")]
    [Route("api/two-factor")]
    public class TwoFactorController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IEmailSender emailSender;
        private readonly ISmsSender smsSender;

        public TwoFactorController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ISmsSender smsSender)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailSender = emailSender;
            this.smsSender = smsSender;
        }

        [HttpGet]
        [Route("providers")]
        public async Task<IActionResult> GetTwoFactorProviders()
        {
            var user = await signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                return BadRequest("Have no user for 2 factor verification.");
            }
            var userFactors = await userManager.GetValidTwoFactorProvidersAsync(user);
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

            var user = await signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                return BadRequest("Have no user for 2 factor verification.");
            }

            // Generate the token and send it
            var code = await userManager.GenerateTwoFactorTokenAsync(user, provider);
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
                await emailSender.SendEmailAsync(await userManager.GetEmailAsync(user), "SecurityCode", parameters);
            }
            else if (provider == "Phone")
            {
                await smsSender.SendSmsAsync(await userManager.GetPhoneNumberAsync(user), "SecurityCode", parameters);
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
            var result = await signInManager.TwoFactorSignInAsync(model.Provider, model.Code, model.RememberLogIn, model.RememberBrowser);
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
}