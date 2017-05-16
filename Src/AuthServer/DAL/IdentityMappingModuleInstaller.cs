using AuthGuard.BLL.Domain.Entities.Identity;
using DddCore.DAL.DomainStack.EntityFramework.Mapping;

namespace AuthGuard.DAL
{
    public class IdentityMappingModuleInstaller : IMappingModuleInstaller
    {
        public void Install(IModelBuilder config)
        {
            config.Entity<IdentityClaim>();

            config
                .Entity<IdentityResourceClaim>()
                .HasOne(x => x.IdentityClaim)
                .WithMany()
                .HasForeignKey(x => x.IdentityClaimId);

            config
                .Entity<IdentityResource>()
                .HasMany(x => x.Claims)
                .WithOne()
                .HasForeignKey(x => x.IdentityResourceId);
        }
    }
}