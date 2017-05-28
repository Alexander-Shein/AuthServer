using AuthGuard.BLL.Domain.Entities.Identity;
using AuthGuard.DAL.QueryRepositories.Identity.Dtos;
using AuthGuard.SL.Contracts.Models.Input.Identity;
using AuthGuard.SL.Contracts.Models.View.Identity;
using DddCore.Crosscutting.ObjectMapper;

namespace AuthGuard.SL.Services.Identity
{
    public class ApiResourcesObjectMapperModuleInstaller : ObjectMapperModuleInstallerBase
    {
        protected override void FromDtoToView()
        {
            Config.Bind<IdentityClaimDto, IdentityClaimVm>(c =>
            {
                c.Bind(x => x.Id, x => x.Id);
                c.Bind(x => x.IsReadOnly, x => x.IsReadOnly);
                c.Bind(x => x.Type, x => x.Type);
            });

            Config.Bind<IdentityResourceDto, IdentityResourceVm>(c =>
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

        protected override void FromDomainToView()
        {
            Config.Bind<IdentityResourceClaim, IdentityClaimVm>(c =>
            {
                c.Bind(x => x.Id, x => x.IdentityClaimId);
            });

            Config.Bind<IdentityResource, IdentityResourceVm>(c =>
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

        protected override void FromInputToDomain()
        {
            Config.Bind<IdentityResourceIm, IdentityResource>(c =>
            {
                c.Bind(x => x.Name, x => x.Name);
                c.Bind(x => x.Description, x => x.Description);
                c.Bind(x => x.DisplayName, x => x.DisplayName);
                c.Bind(x => x.Emphasize, x => x.Emphasize);
                c.Bind(x => x.IsEnabled, x => x.IsEnabled);
                c.Bind(x => x.IsRequired, x => x.IsRequired);
                c.Bind(x => x.ShowInDiscoveryDocument, x => x.ShowInDiscoveryDocument);
                c.Bind(x => x.Claims, x => x.Claims);
            });

            Config.Bind<IdentityClaimIm, IdentityResourceClaim>(c =>
            {
                c.Bind(x => x.IdentityClaimId, x => x.Id);
                c.Ignore(x => x.Id);
            });
        }
    }
}