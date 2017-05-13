using AuthGuard.BLL.Domain.Entities;
using DddCore.DAL.DomainStack.EntityFramework.Mapping;
using Microsoft.EntityFrameworkCore;

namespace AuthGuard.DAL
{
    public class MappingModuleInstaller : IMappingModuleInstaller
    {
        public void Install(IModelBuilder config)
        {
            config
                .Entity<Email>()
                .HasOne(x => x.EmailTemplate).WithMany().HasForeignKey(c => c.EmailTemplateId); ;

            config
                .Entity<EmailTemplate>(b =>
                {
                    b.Property(x => x.EmailBodyFormat).HasColumnName("EmailBodyFormatId");
                    b.Property(x => x.Template).HasColumnName("TemplateId");
                });

            config
                .Entity<Sms>()
                .HasOne(x => x.SmsTemplate).WithMany().HasForeignKey(c => c.SmsTemplateId);

            config
                .Entity<SmsTemplate>()
                .Property(x => x.Template).HasColumnName("TemplateId");

            config
                .Entity<SecurityCode>(c =>
                {
                    c.HasMany(x => x.Parameters).WithOne().HasForeignKey(x => x.SecurityCodeId);
                    c.Property(x => x.SecurityCodeAction).HasColumnName("SecurityCodeActionId");
                });

            config
                .Entity<SecurityCodeParameter>()
                .Ignore(x => x.Name)
                .Property(x => x.NameString).HasColumnName("Name");

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