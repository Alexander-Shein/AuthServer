using AuthGuard.BLL.Domain.Entities.Identity.BusinessRules;
using AuthGuard.DAL.QueryRepositories.Identity;
using DddCore.Contracts.Crosscutting.DependencyInjection.Base;
using Microsoft.Extensions.DependencyInjection;

namespace AuthGuard.SL.Services.Identity
{
    public class IdentityDiModuleInstaller : IDiModuleInstaller
    {
        public void Install(IServiceCollection config)
        {
            config.AddScoped<IIdentityClaimsExistValidator, IdentityClaimsExistValidator>();
            config.AddScoped<IIdentityResourceUniqueNameValidator, IdentityResourceUniqueNameValidator>();
        }
    }
}