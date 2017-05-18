using AuthGuard.BLL.Domain.Entities.Identity;
using AuthGuard.DAL.QueryRepositories.Identity.Dtos;
using AuthGuard.SL.Contracts.Models.View.Identity;
using DddCore.Contracts.Crosscutting.ObjectMapper;
using DddCore.Contracts.Crosscutting.ObjectMapper.Base;

namespace AuthGuard.SL.Services.Identity
{
    public class IdentityObjectMapperModuleInstaller : IObjectMapperModuleInstaller
    {
        public void Install(IObjectMapperConfig config)
        {
            config.Bind<IdentityClaimDto, IdentityClaimVm>(c =>
            {
                c.Bind(x => x.Id, x => x.Id);
                c.Bind(x => x.IsReadOnly, x => x.IsReadOnly);
                c.Bind(x => x.Type, x => x.Type);
            });

            config.Bind<IdentityResourceDto, IdentityResourceVm>(c =>
            {
                c.Bind(x => x.Id, x => x.Id);
                c.Bind(x => x.Description, x => x.Description);
                c.Bind(x => x.DisplayName, x => x.DisplayName);
                c.Bind(x => x.Emphasize, x => x.Emphasize);
                c.Bind(x => x.IsReadOnly, x => x.IsReadOnly);
                c.Bind(x => x.IsRequired, x => x.IsRequired);
                c.Bind(x => x.IsEnabled, x => x.IsEnabled);
                c.Bind(x => x.Name, x => x.Name);
                c.Bind(x => x.ShowInDiscoveryDocument, x => x.ShowInDiscoveryDocument);
                c.Bind(x => x.Claims, x => x.Claims);
            });

            config.Bind<IdentityResourceClaim, IdentityClaimVm>(c =>
            {
                c.Bind(x => x.IdentityClaim.Id, x => x.Id);
                c.Bind(x => x.IdentityClaim.IsReadOnly, x => x.IsReadOnly);
                c.Bind(x => x.IdentityClaim.IsEnabled, x => x.IsEnabled);
            });

            config.Bind<IdentityResource, IdentityResourceVm>(c =>
            {
                c.Bind(x => x.Id, x => x.Id);
                c.Bind(x => x.Description, x => x.Description);
                c.Bind(x => x.DisplayName, x => x.DisplayName);
                c.Bind(x => x.Emphasize, x => x.Emphasize);
                c.Bind(x => x.IsReadOnly, x => x.IsReadOnly);
                c.Bind(x => x.IsRequired, x => x.IsRequired);
                c.Bind(x => x.IsEnabled, x => x.IsEnabled);
                c.Bind(x => x.Name, x => x.Name);
                c.Bind(x => x.ShowInDiscoveryDocument, x => x.ShowInDiscoveryDocument);
                c.Bind(x => x.Claims, x => x.Claims);
            });
        }
    }
}