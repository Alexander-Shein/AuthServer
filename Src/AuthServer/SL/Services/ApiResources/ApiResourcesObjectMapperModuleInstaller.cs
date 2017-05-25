using AuthGuard.BLL.Domain.Entities.ApiResources;
using AuthGuard.DAL.QueryRepositories.ApiResources.Dtos;
using AuthGuard.SL.Contracts.Models.View.ApiServices;
using DddCore.Contracts.Crosscutting.ObjectMapper;
using DddCore.Contracts.Crosscutting.ObjectMapper.Base;

namespace AuthGuard.SL.Services.ApiServices
{
    public class ApiResourcesObjectMapperModuleInstaller : IObjectMapperModuleInstaller
    {
        public void Install(IObjectMapperConfig config)
        {
            config.Bind<ApiScopeClaimDto, ApiScopeClaimVm>(c =>
            {
                c.Bind(x => x.Id, x => x.Id);
                c.Bind(x => x.Type, x => x.Type);
                c.Bind(x => x.Description, x => x.Description);
            });

            config.Bind<ApiScopeDto, ApiScopeVm>(c =>
            {
                c.Bind(x => x.Id, x => x.Id);
                c.Bind(x => x.Name, x => x.Name);
                c.Bind(x => x.DisplayName, x => x.DisplayName);
                c.Bind(x => x.Description, x => x.Description);
                c.Bind(x => x.Emphasize, x => x.Emphasize);
                c.Bind(x => x.IsEnabled, x => x.IsEnabled);
                c.Bind(x => x.IsRequired, x => x.IsRequired);
                c.Bind(x => x.UserClaims, x => x.UserClaims);
            });

            config.Bind<ApiResourceDto, ApiResourceVm>(c =>
            {
                c.Bind(x => x.Id, x => x.Id);
                c.Bind(x => x.Name, x => x.Name);
                c.Bind(x => x.DisplayName, x => x.DisplayName);
                c.Bind(x => x.IsEnabled, x => x.IsEnabled);
                c.Bind(x => x.Description, x => x.Description);
                c.Bind(x => x.Secret, x => x.Secret);
                c.Bind(x => x.Scopes, x => x.Scopes);
            });

            config.Bind<ApiScopeClaim, ApiScopeClaimVm>(c =>
            {
                c.Bind(x => x.IdentityClaim.Id, x => x.Id);
                c.Bind(x => x.IdentityClaim.Type, x => x.Type);
                c.Bind(x => x.IdentityClaim.Description, x => x.Description);
            });

            config.Bind<ApiScope, ApiScopeVm>(c =>
            {
                c.Bind(x => x.Id, x => x.Id);
                c.Bind(x => x.Name, x => x.Name);
                c.Bind(x => x.DisplayName, x => x.DisplayName);
                c.Bind(x => x.Description, x => x.Description);
                c.Bind(x => x.Emphasize, x => x.Emphasize);
                c.Bind(x => x.IsEnabled, x => x.IsEnabled);
                c.Bind(x => x.IsRequired, x => x.IsRequired);
                c.Bind(x => x.UserClaims, x => x.UserClaims);
            });

            config.Bind<ApiResource, ApiResourceVm>(c =>
            {
                c.Bind(x => x.Id, x => x.Id);
                c.Bind(x => x.Name, x => x.Name);
                c.Bind(x => x.DisplayName, x => x.DisplayName);
                c.Bind(x => x.IsEnabled, x => x.IsEnabled);
                c.Bind(x => x.Description, x => x.Description);
                c.Bind(x => x.Secret, x => x.Secret);
                c.Bind(x => x.Scopes, x => x.Scopes);
            });
        }
    }
}