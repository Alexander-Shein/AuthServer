using AuthGuard.BLL.Domain.Entities;
using DddCore.DAL.DomainStack.EntityFramework.Mapping;

namespace AuthGuard.DAL
{
    public class MappingModuleInstaller : IMappingModuleInstaller
    {
        public void Install(IModelBuilder config)
        {
            config
                .Entity<App>()
                .HasMany(x => x.ExternalProviders).WithOne().HasForeignKey(x => x.AppId);

            config
                .Entity<AppExternalProvider>()
                .HasOne(x => x.ExternalProvider).WithMany().HasForeignKey(x => x.ExternalProviderId);

            config.Entity<ExternalProvider>();
        }
    }
}