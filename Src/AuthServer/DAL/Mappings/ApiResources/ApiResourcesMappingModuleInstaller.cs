using System;
using AuthGuard.BLL.Domain.Entities.ApiResources;
using DddCore.DAL.DomainStack.EntityFramework.Mapping;

namespace AuthGuard.DAL.Mappings.ApiResources
{
    public class ApiResourcesMappingModuleInstaller : IMappingModuleInstaller
    {
        public void Install(IModelBuilder config)
        {
            config
                .Entity<ApiScopeClaim>(c =>
                {
                    c.Property<Guid>("IdentityClaimId");
                    c.Property<Guid>("ApiScopeId");
                    c.HasOne(x => x.IdentityClaim).WithMany().HasForeignKey("IdentityClaimId");
                });

            config
                .Entity<ApiScope>(c =>
                {
                    c.Property<Guid>("ApiResourceId");
                    c.HasMany(x => x.UserClaims).WithOne().HasForeignKey("ApiScopeId");
                });

            config
                .Entity<ApiResource>()
                .HasMany(x => x.Scopes)
                .WithOne()
                .HasForeignKey("ApiResourceId");
        }
    }
}