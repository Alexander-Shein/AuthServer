using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthGuard.BLL.Domain.Entities;
using AuthGuard.Data;
using DddCore.Contracts.BLL.Errors;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.EntityFrameworkCore;
using DddCore.Contracts.Crosscutting.UserContext;

namespace AuthGuard.Services.Apps
{
    public interface IAppsService : IClientStore
    {
        Task<AppVm> GetAuthGuardApp();
        Task<AppVm> Search(string returnUrl);
        Task<(ExtendedAppVm App, OperationResult OperationResult)> Put(Guid id, ExtendedAppIm im);
        Task<IEnumerable<ExtendedAppVm>> GetAll();
        Task<ExtendedAppVm> Get(Guid id);
    }

    public class AppsService : IAppsService
    {
        readonly ApplicationDbContext context;
        readonly IIdentityServerInteractionService interaction;
        readonly IUserContext<Guid> userContext;

        readonly string[] allowedScopes =
        {
            IdentityServerConstants.StandardScopes.OpenId,
            IdentityServerConstants.StandardScopes.Profile,
            IdentityServerConstants.StandardScopes.Email
        };

        public AppsService(ApplicationDbContext context, IIdentityServerInteractionService interaction, IUserContext<Guid> userContext)
        {
            this.context = context;
            this.interaction = interaction;
            this.userContext = userContext;
        }

        public async Task<Client> FindClientByIdAsync(string clientId)
        {
            var app = await context.Set<App>().FirstOrDefaultAsync(x => x.Key == clientId);

            if (app == null) return null;

            var client = new Client
            {
                ClientUri = app.WebsiteUrl,
                AllowedScopes = allowedScopes,
                ClientId = clientId,
                AllowOfflineAccess = true,
                AccessTokenType = AccessTokenType.Jwt,
                ClientName = app.DisplayName,
                EnableLocalLogin = app.IsLocalAccountEnabled,
                Enabled = app.IsActive,
                RequireClientSecret = false
            };
            return client;
        }

        public async Task<AppVm> Search(string returnUrl)
        {
            var authorizationContext = await interaction.GetAuthorizationContextAsync(returnUrl);

            if (authorizationContext?.ClientId == null) return null;
            var app = await context.Set<App>().FirstOrDefaultAsync(x => x.Key == authorizationContext.ClientId);

            return Map(app);
        }

        public async Task<(ExtendedAppVm App, OperationResult OperationResult)> Put(Guid id, ExtendedAppIm im)
        {
            var app = await context.Set<App>().FindAsync(id);

            if (app == null)
            {
                app = new App
                {
                    CreatedAt = DateTime.UtcNow,
                    UserId = userContext.Id.ToString()
            };
                context.Set<App>().Add(app);
            }
            else
            {
                context.Set<App>().Update(app);
            }

            app.IsActive = im.IsActive;
            app.EmailSettings.IsEnabled = im.EmailSettings.IsEnabled;
            app.EmailSettings.IsConfirmationRequired = im.EmailSettings.IsConfirmationRequired;
            app.EmailSettings.IsPasswordEnabled = im.EmailSettings.IsPasswordEnabled;
            app.EmailSettings.IsPasswordlessEnabled = im.EmailSettings.IsPasswordlessEnabled;
            app.EmailSettings.IsSearchRelatedProviderEnabled = im.EmailSettings.IsSearchRelatedProviderEnabled;
            app.IsSecurityQuestionsEnabled = im.IsSecurityQuestionsEnabled;
            app.Key = im.Key;
            app.WebsiteUrl = im.WebsiteUrl;
            app.DisplayName = im.Name;
            app.IsRememberLogInEnabled = im.IsRememberLogInEnabled;
            app.PhoneSettings.IsEnabled = im.PhoneSettings.IsEnabled;
            app.PhoneSettings.IsConfirmationRequired = im.PhoneSettings.IsConfirmationRequired;
            app.PhoneSettings.IsPasswordEnabled = im.PhoneSettings.IsPasswordEnabled;
            app.PhoneSettings.IsPasswordlessEnabled = im.PhoneSettings.IsPasswordlessEnabled;

            await context.SaveChangesAsync();

            return (MapToExtendedAppVm(app), OperationResult.SucceedResult);
        }

        public async Task<IEnumerable<ExtendedAppVm>> GetAll()
        {
            var userId = userContext.Id.ToString();
            var apps = await context.Set<App>().Where(x => x.UserId == userId).ToListAsync() ?? Enumerable.Empty<App>();

            return apps.Select(MapToExtendedAppVm);
        }

        public async Task<ExtendedAppVm> Get(Guid id)
        {
            var userId = userContext.Id.ToString();

            var app = await context.Set<App>().FindAsync(id);
            if (app == null || app.UserId == userId) return null;

            var result = MapToExtendedAppVm(app);
            return result;
        }

        public async Task<AppVm> GetAuthGuardApp()
        {
            var app = await context.Set<App>().FirstOrDefaultAsync(x => x.Key == "auth-guard");
            return Map(app);
        }

        #region Private Methods

        private AppVm Map(App app)
        {
            return new AppVm
            {
                Key = app.Key,
                IsLocalAccountEnabled = app.IsLocalAccountEnabled,
                Name = app.DisplayName,
                IsRememberLogInEnabled = app.IsRememberLogInEnabled,
                IsSecurityQuestionsEnabled = app.IsSecurityQuestionsEnabled,
                EmailSettings = new LocalAccountSettingsVm
                {
                    IsEnabled = app.EmailSettings.IsEnabled,
                    IsPasswordEnabled = app.EmailSettings.IsPasswordEnabled,
                    IsSearchRelatedProviderEnabled = app.EmailSettings.IsSearchRelatedProviderEnabled,
                    IsConfirmationRequired = app.EmailSettings.IsConfirmationRequired,
                    IsPasswordlessEnabled = app.EmailSettings.IsPasswordlessEnabled
                },
                PhoneSettings = new LocalAccountSettingsVm
                {
                    IsEnabled = app.PhoneSettings.IsEnabled,
                    IsPasswordEnabled = app.PhoneSettings.IsPasswordEnabled,
                    IsConfirmationRequired = app.PhoneSettings.IsConfirmationRequired,
                    IsPasswordlessEnabled = app.PhoneSettings.IsPasswordlessEnabled
                },
                ExternalProviders =
                    app
                        .ExternalProviders
                        .Select(x => new ExternalProviderVm
                        {
                            DisplayName = x.ExternalProvider.DisplayName,
                            AuthenticationScheme = x.ExternalProvider.AuthenticationScheme
                        })
            };
        }

        private ExtendedAppVm MapToExtendedAppVm(App app)
        {
            return new ExtendedAppVm
            {
                Id = app.Id,
                Key = app.Key,
                IsLocalAccountEnabled = app.IsLocalAccountEnabled,
                Name = app.DisplayName,
                IsRememberLogInEnabled = app.IsRememberLogInEnabled,
                IsSecurityQuestionsEnabled = app.IsSecurityQuestionsEnabled,
                UsersCount = app.UsersCount,
                IsActive = app.IsActive,
                WebsiteUrl = app.WebsiteUrl,
                EmailSettings = new LocalAccountSettingsVm
                {
                    IsEnabled = app.EmailSettings.IsEnabled,
                    IsPasswordEnabled = app.EmailSettings.IsPasswordEnabled,
                    IsSearchRelatedProviderEnabled = app.EmailSettings.IsSearchRelatedProviderEnabled,
                    IsConfirmationRequired = app.EmailSettings.IsConfirmationRequired,
                    IsPasswordlessEnabled = app.EmailSettings.IsPasswordlessEnabled
                },
                PhoneSettings = new LocalAccountSettingsVm
                {
                    IsEnabled = app.PhoneSettings.IsEnabled,
                    IsPasswordEnabled = app.PhoneSettings.IsPasswordEnabled,
                    IsConfirmationRequired = app.PhoneSettings.IsConfirmationRequired,
                    IsPasswordlessEnabled = app.PhoneSettings.IsPasswordlessEnabled
                },
                ExternalProviders =
                    app
                        .ExternalProviders
                        .Select(x => new ExternalProviderVm
                        {
                            DisplayName = x.ExternalProvider.DisplayName,
                            AuthenticationScheme = x.ExternalProvider.AuthenticationScheme
                        })
            };
        }

        #endregion
    }
}