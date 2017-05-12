using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthGuard.BLL.Domain.Entities;
using AuthGuard.Data;
using AuthGuard.DAL.QueryRepositories.Apps;
using AuthGuard.DAL.Repositories.Apps;
using DddCore.Contracts.BLL.Errors;
using DddCore.Contracts.Crosscutting.UserContext;
using DddCore.Contracts.DAL.DomainStack;
using IdentityServer4.Services;
using Microsoft.EntityFrameworkCore;

namespace AuthGuard.SL.Apps
{
    public class AppsWorkflowService : IAppsWorkflowService
    {
        readonly ApplicationDbContext context;
        readonly IIdentityServerInteractionService interaction;
        readonly IAppsEntityService appsEntityService;
        readonly IUnitOfWork unitOfWork;
        readonly IAppsRepository appsRepository;
        readonly IUserContext<Guid> userContext;
        readonly IAppsQueryRepository appsQueryRepository;

        const string AuthGuardKey = "auth-guard";

        public AppsWorkflowService(
            ApplicationDbContext context,
            IIdentityServerInteractionService interaction,
            IAppsEntityService appsEntityService,
            IUnitOfWork unitOfWork,
            IAppsRepository appsRepository,
            IUserContext<Guid> userContext,
            IAppsQueryRepository appsQueryRepository)
        {
            this.context = context;
            this.interaction = interaction;
            this.appsEntityService = appsEntityService;
            this.unitOfWork = unitOfWork;
            this.appsRepository = appsRepository;
            this.userContext = userContext;
            this.appsQueryRepository = appsQueryRepository;
        }

        public async Task<AppVm> SearchAsync(string returnUrl)
        {
            var authorizationContext = await interaction.GetAuthorizationContextAsync(returnUrl);

            if (authorizationContext?.ClientId == null) return null;

            var app = await appsRepository.ReadWithProvidersByKey(authorizationContext.ClientId);
            return Map(app);
        }

        public async Task<(ExtendedAppVm App, OperationResult OperationResult)> PutAsync(Guid id, ExtendedAppIm im)
        {
            var result = await appsEntityService.PutAsync(id, im);

            if (result.OperationResult.IsSucceed)
            {
                await unitOfWork.SaveAsync();
            }

            return (MapToExtendedAppVm(result.App), result.OperationResult);
        }

        public async Task<IEnumerable<ExtendedAppVm>> GetAllAsync()
        {
            var userId = userContext.Id.ToString();

            var apps = await context.Set<App>().Where(x => x.UserId == userId).ToListAsync() ?? Enumerable.Empty<App>();

            return apps.Select(MapToExtendedAppVm);
        }

        public async Task<ExtendedAppVm> GetAsync(Guid id)
        {
            var userId = userContext.Id.ToString();

            var app = await appsRepository.ReadWithProvidersById(id);
            if (app == null || app.UserId != userId) return null;

            var result = MapToExtendedAppVm(app);
            return result;
        }

        public async Task<bool> IsAppExistAsync(string key)
        {
            if (String.IsNullOrWhiteSpace(key)) return false;

            return await appsQueryRepository.IsAppExist(key);
        }

        public async Task<OperationResult> DeleteAsync(Guid id)
        {
            var result = await appsEntityService.DeleteAsync(id);

            if (result.IsSucceed)
            {
                await unitOfWork.SaveAsync();
            }

            return result;
        }

        public async Task<AppVm> GetAuthGuardApp()
        {
            var app =
                await
                    appsRepository.ReadWithProvidersByKey(AuthGuardKey)
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