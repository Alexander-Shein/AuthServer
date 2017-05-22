using System;
using AuthGuard.BLL.Domain.Entities.Identity;
using DddCore.DAL.DomainStack.EntityFramework.Mapping;

namespace AuthGuard.DAL.Mappings.Identity
{
    public class IdentityMappingModuleInstaller : IMappingModuleInstaller
    {
        public void Install(IModelBuilder config)
        {
            config.Entity<IdentityClaim>();

            config
                .Entity<IdentityResourceClaim>(c =>
                {
                    c.Property<Guid>("IdentityClaimId");
                    c.Property<Guid>("IdentityResourceId");
                    c.HasOne(x => x.IdentityClaim).WithMany().HasForeignKey("IdentityClaimId");
                });

            config
                .Entity<IdentityResource>()
                .HasMany(x => x.Claims)
                .WithOne()
                .HasForeignKey("IdentityResourceId");
        }
    }
}