using System;
using System.Linq;
using System.Threading.Tasks;
using AuthGuard.BLL.Domain.Entities;
using AuthGuard.DAL.Repositories.Apps;
using DddCore.Contracts.BLL.Domain.Entities;
using DddCore.Contracts.BLL.Domain.Entities.BusinessRules;
using DddCore.Contracts.BLL.Domain.Entities.State;
using DddCore.Contracts.BLL.Domain.Events;
using DddCore.Contracts.BLL.Errors;
using DddCore.Contracts.Crosscutting.UserContext;
using DddCore.Contracts.SL.Services.Application.DomainStack;
using DddCore.Contracts.SL.Services.Infrastructure;

namespace AuthGuard.SL.Apps
{
    public class AppsEntityService : IAppsEntityService, IInfrastructureService
    {
        readonly IAppsRepository appsRepository;
        readonly IUserContext<Guid> userContext;
        readonly IDomainEventDispatcher domainEventDispatcher;
        readonly IBusinessRulesValidatorFactory businessRulesValidatorFactory;

        public AppsEntityService(
            IAppsRepository repository,
            IUserContext<Guid> userContext,
            IDomainEventDispatcher domainEventDispatcher,
            IBusinessRulesValidatorFactory businessRulesValidatorFactory)
        {
            appsRepository = repository;
            this.userContext = userContext;
            this.domainEventDispatcher = domainEventDispatcher;
            this.businessRulesValidatorFactory = businessRulesValidatorFactory;
        }

        public async Task<(App App, OperationResult OperationResult)> PutAsync(Guid id, ExtendedAppIm im)
        {
            var app = await appsRepository.GetByIdAsync(id);

            if (app == null)
            {
                app = new App
                {
                    UserId = userContext.Id.ToString(),
                    CrudState = CrudState.Added
                };
            }
            else
            {
                app.CrudState = CrudState.Modified;
            }

            app.IsLocalAccountEnabled = im.IsLocalAccountEnabled;
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

            var newProviders = im.ExternalProviders.ToList();

            foreach (var appExternalProvider in app.ExternalProviders)
            {
                var newProvider = newProviders.Where(x => x.Id == appExternalProvider.Id).ToList();

                if (newProvider.Any())
                {
                    foreach (var p in newProvider)
                    {
                        newProviders.Remove(p);
                    }
                }
                else
                {
                    appExternalProvider.CrudState = CrudState.Deleted;
                }
            }

            foreach (var p in newProviders)
            {
                var externalProvider = new AppExternalProvider
                {
                    AppId = app.Id,
                    ExternalProviderId = p.Id,
                    CrudState = CrudState.Added
                };

                app.ExternalProviders.Add(externalProvider);
            }

            var result = app.Validate(businessRulesValidatorFactory);
            appsRepository.Persist(app);

            if (result.IsSucceed)
            {
                return (app, result);
            }

            return (null, result);
        }

        public async Task<OperationResult> DeleteAsync(Guid id)
        {
            var app = await appsRepository.ReadWithProvidersById(id);

            if (app == null)
            {
                return OperationResult.Failed(1, $"App with id '{id}' does not exist.");
            }

            if (app.UserId != userContext.Id.ToString())
            {
                return OperationResult.Failed(2, "App does not belong to the current user.");
            }

            app.WalkGraph(x => x.CrudState = CrudState.Deleted);
            appsRepository.Persist(app);

            return OperationResult.Succeed;
        }
    }
}