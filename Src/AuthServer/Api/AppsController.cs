using System.Collections.Generic;
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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthServer.Api
{
    [EnableCors("default")]
    [Route("api/[controller]")]
    public class AppsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly AccountService _account;

        public AppsController(
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

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> Get(string returnUrl)
        {
            var vm = await _account.BuildLoginViewModelAsync(returnUrl);

            return Ok(new AppVm
            {
                Name = vm.Client?.ClientName ?? "AuthGuardian",
                Key = vm.Client?.ClientId ?? "auth-guardian",
                IsLocalAccountEnabled = vm.EnableLocalLogin,
                IsRememberLogInEnabled = true,
                ExternalProviders = vm.ExternalProviders,
                EmailSettings = new LocalAccountSettings
                {
                    IsEnabled = true
                },
                PhoneSettings = new LocalAccountSettings
                {
                    IsEnabled = true
                }
            });
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    public class AppVm
    {
        public string Name { get; set; }
        public string Key { get; set; }
        public bool IsLocalAccountEnabled { get; set; }
        public bool IsRememberLogInEnabled { get; set; }
        public LocalAccountSettings EmailSettings { get; set; }
        public LocalAccountSettings PhoneSettings { get; set; }
        public IEnumerable<ExternalProvider> ExternalProviders { get; set; }
    }

    public class LocalAccountSettings
    {
        public bool IsEnabled { get; set; }
        public bool IsPasswordlessEnabled { get; set; }
        public bool IsPasswordEnabled { get; set; }
        public bool IsConfirmationRequired { get; set; }
        public bool IsSearchRelatedProviderEnabled { get; set; }
    }
}