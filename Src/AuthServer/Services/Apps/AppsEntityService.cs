using System;
using System.Linq;
using System.Threading.Tasks;
using AuthGuard.BLL.Domain.Entities;
using AuthGuard.DAL.Repositories.Apps;
using DddCore.Contracts.BLL.Domain.Entities;
using DddCore.Contracts.BLL.Domain.Entities.BusinessRules;
using DddCore.Contracts.BLL.Domain.Entities.Model;
using DddCore.Contracts.BLL.Domain.Events;
using DddCore.Contracts.BLL.Errors;
using DddCore.Contracts.Crosscutting.UserContext;
using DddCore.Contracts.SL.Services.Application.DomainStack;
using DddCore.SL.Services.Application.DomainStack;

namespace AuthGuard.Services.Apps
{
    public class AppsEntityService : EntityService<App, Guid>, IAppsEntityService
    {
        readonly IAppsRepository appsRepository;
        readonly IDomainFactory domainFactory;
        readonly IUserContext<Guid> userContext;
        readonly IDomainEventDispatcher domainEventDispatcher;
        readonly IBusinessRulesValidatorFactory businessRulesValidatorFactory;

        public AppsEntityService(
            IAppsRepository repository,
            IGuard guard,
            IDomainFactory domainFactory,
            IUserContext<Guid> userContext,
            IDomainEventDispatcher domainEventDispatcher,
            IBusinessRulesValidatorFactory businessRulesValidatorFactory) : base(repository, guard)
        {
            appsRepository = repository;
            this.domainFactory = domainFactory;
            this.userContext = userContext;
            this.domainEventDispatcher = domainEventDispatcher;
            this.businessRulesValidatorFactory = businessRulesValidatorFactory;
        }

        public async Task<(App App, OperationResult OperationResult)> PutAsync(Guid id, ExtendedAppIm im)
        {
            var app = await appsRepository.ReadByIdAsync(id);

            if (app == null)
            {
                app = domainFactory.Create<App, Guid>();
                app.UserId = userContext.Id.ToString();
                app.CrudState = CrudState.Added;
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
                var externalProvider = domainFactory.Create<AppExternalProvider, Guid>();
                externalProvider.AppId = app.Id;
                externalProvider.ExternalProviderId = p.Id;
                externalProvider.CrudState = CrudState.Added;

                app.ExternalProviders.Add(externalProvider);
            }

            app.BusinessRulesValidatorFactory = businessRulesValidatorFactory;
            app.DomainEventDispatcher = domainEventDispatcher;

            var result = ValidateAndPersist(app);

            if (result.IsSucceed)
            {
                return (app, result);
            }

            return (null, result);
        }

        public async Task<OperationResult> DeleteAsync(Guid id)
        {
            var app = await appsRepository.ReadWithProvidersById(id);

            if (app == null) return OperationResult.Succeed;

            if (app.UserId != userContext.Id.ToString())
            {
                return OperationResult.Failed(1, "App does not belong to the current user.");
            }

            app.BusinessRulesValidatorFactory = businessRulesValidatorFactory;
            app.DomainEventDispatcher = domainEventDispatcher;

            app.WalkGraph(x => x.CrudState = CrudState.Deleted);
            appsRepository.Persist(app);

            return OperationResult.Succeed;
        }
    }
}