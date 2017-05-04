using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthGuard.BLL.Domain.Entities;
using AuthGuard.Data;
using AuthGuard.Services.Users;
using DddCore.Contracts.BLL.Errors;
using IdentityServer4.Services;
using Microsoft.EntityFrameworkCore;
using DddCore.Contracts.Crosscutting.UserContext;
using Microsoft.AspNetCore.Http;

namespace AuthGuard.Services.Apps
{
    public class AppsService : IAppsService
    {
        readonly ApplicationDbContext context;
        readonly IIdentityServerInteractionService interaction;
        readonly IUserContext<Guid> userContext;
        readonly IHttpContextAccessor httpContextAccessor;
        readonly IUsersService usersService;

        public AppsService(
            ApplicationDbContext context,
            IIdentityServerInteractionService interaction,
            IUserContext<Guid> userContext,
            IHttpContextAccessor httpContextAccessor,
            IUsersService usersService)
        {
            this.context = context;
            this.interaction = interaction;
            this.userContext = userContext;
            this.httpContextAccessor = httpContextAccessor;
            this.usersService = usersService;
        }

        public async Task<AppVm> SearchAsync(string returnUrl)
        {
            var authorizationContext = await interaction.GetAuthorizationContextAsync(returnUrl);

            if (authorizationContext?.ClientId == null) return null;
            var app =
                await
                    context
                        .Set<App>()
                        .Include(x => x.ExternalProviders)
                            .ThenInclude(x => x.ExternalProvider)
                        .FirstOrDefaultAsync(x => x.Key == authorizationContext.ClientId);

            return Map(app);
        }

        public async Task<(ExtendedAppVm App, OperationResult OperationResult)> PutAsync(Guid id, ExtendedAppIm im)
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
            app.IsEmailEnabled = im.EmailSettings.IsEnabled;
            app.IsEmailConfirmationRequired = im.EmailSettings.IsConfirmationRequired;
            app.IsEmailPasswordEnabled = im.EmailSettings.IsPasswordEnabled;
            app.IsEmailPasswordlessEnabled = im.EmailSettings.IsPasswordlessEnabled;
            app.IsEmailSearchRelatedProviderEnabled = im.EmailSettings.IsSearchRelatedProviderEnabled;
            app.IsSecurityQuestionsEnabled = im.IsSecurityQuestionsEnabled;
            app.Key = im.Key;
            app.WebsiteUrl = im.WebsiteUrl;
            app.DisplayName = im.Name;
            app.IsRememberLogInEnabled = im.IsRememberLogInEnabled;
            app.IsPhoneEnabled = im.PhoneSettings.IsEnabled;
            app.IsPhoneConfirmationRequired = im.PhoneSettings.IsConfirmationRequired;
            app.IsPhonePasswordEnabled = im.PhoneSettings.IsPasswordEnabled;
            app.IsPhonePasswordlessEnabled = im.PhoneSettings.IsPasswordlessEnabled;

            await context.SaveChangesAsync();

            return (MapToExtendedAppVm(app), OperationResult.SucceedResult);
        }

        public async Task<IEnumerable<ExtendedAppVm>> GetAllAsync()
        {
            var claims = httpContextAccessor.HttpContext.User;
            var userId = (await usersService.GetCurrentUserAsync(claims)).Id.ToString();

            var apps = await context.Set<App>().Where(x => x.UserId == userId).ToListAsync() ?? Enumerable.Empty<App>();

            return apps.Select(MapToExtendedAppVm);
        }

        public async Task<ExtendedAppVm> GetAsync(Guid id)
        {
            var userId = userContext.Id.ToString();

            var app = await context.Set<App>().FindAsync(id);
            if (app == null || app.UserId == userId) return null;

            var result = MapToExtendedAppVm(app);
            return result;
        }

        public async Task<AppVm> GetAuthGuardApp()
        {
            var app =
                await
                    context
                        .Set<App>()
                        .Include(x => x.ExternalProviders)
                            .ThenInclude(x => x.ExternalProvider)
                        .FirstOrDefaultAsync(x => x.Key == "auth-guard")
                        ?? throw new ArgumentException("'auth-guard' app is missed in DataBase. Please add it to DataBase.");

            return Map(app);
        }

        #region Private Methods

        private AppVm Map(App app)
        {
            return new AppVm
            {
                Id = app.Id,
                Key = app.Key,
                IsLocalAccountEnabled = app.IsLocalAccountEnabled,
                Name = app.DisplayName,
                IsRememberLogInEnabled = app.IsRememberLogInEnabled,
                IsSecurityQuestionsEnabled = app.IsSecurityQuestionsEnabled,
                EmailSettings = new LocalAccountSettingsVm
                {
                    IsEnabled = app.IsEmailEnabled,
                    IsPasswordEnabled = app.IsEmailPasswordEnabled,
                    IsSearchRelatedProviderEnabled = app.IsEmailSearchRelatedProviderEnabled,
                    IsConfirmationRequired = app.IsEmailConfirmationRequired,
                    IsPasswordlessEnabled = app.IsEmailPasswordlessEnabled
                },
                PhoneSettings = new LocalAccountSettingsVm
                {
                    IsEnabled = app.IsPhoneEnabled,
                    IsPasswordEnabled = app.IsPhonePasswordEnabled,
                    IsConfirmationRequired = app.IsPhoneConfirmationRequired,
                    IsPasswordlessEnabled = app.IsPhonePasswordlessEnabled
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
                    IsEnabled = app.IsEmailEnabled,
                    IsPasswordEnabled = app.IsEmailPasswordEnabled,
                    IsSearchRelatedProviderEnabled = app.IsEmailSearchRelatedProviderEnabled,
                    IsConfirmationRequired = app.IsEmailConfirmationRequired,
                    IsPasswordlessEnabled = app.IsEmailPasswordlessEnabled
                },
                PhoneSettings = new LocalAccountSettingsVm
                {
                    IsEnabled = app.IsPhoneEnabled,
                    IsPasswordEnabled = app.IsPhonePasswordEnabled,
                    IsConfirmationRequired = app.IsPhoneConfirmationRequired,
                    IsPasswordlessEnabled = app.IsPhonePasswordlessEnabled
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