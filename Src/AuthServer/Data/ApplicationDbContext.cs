using AuthGuard.BLL.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthGuard.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<Email>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.Body);
                b.Property(x => x.Subject);
                b.Property(x => x.FromName);
                b.Property(x => x.FromEmail);
                b.Property(x => x.IsSent);
                b.Property(x => x.ToEmail);
                b.Property(x => x.CreatedAt);
                b.HasOne(x => x.EmailTemplate).WithMany().HasForeignKey(c => c.EmailTemplateId);

                b.Ignore(x => x.CrudState);
                b.Ignore(x => x.Events);

                b.ToTable("Email");
            });

            builder.Entity<EmailTemplate>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.FromNameTemplate);
                b.Property(x => x.SubjectTemplate);
                b.Property(x => x.BodyTemplate);
                b.Property(x => x.FromEmail);
                b.Property(x => x.IsActive);
                b.Property(x => x.EmailBodyFormat).HasColumnName("EmailBodyFormatId");
                b.Property(x => x.Template).HasColumnName("TemplateId");

                b.Ignore(x => x.CrudState);
                b.Ignore(x => x.Events);

                b.ToTable("EmailTemplate");
            });

            builder.Entity<Sms>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.FromPhoneNumber);
                b.Property(x => x.Message);
                b.Property(x => x.IsSent);
                b.Property(x => x.ToPhoneNumber);
                b.Property(x => x.CreatedAt);
                b.HasOne(x => x.SmsTemplate).WithMany().HasForeignKey(c => c.SmsTemplateId);

                b.Ignore(x => x.CrudState);
                b.Ignore(x => x.Events);

                b.ToTable("Sms");
            });

            builder.Entity<SmsTemplate>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.FromPhoneNumber);
                b.Property(x => x.MessageTemplate);
                b.Property(x => x.IsActive);
                b.Property(x => x.Template).HasColumnName("TemplateId");

                b.Ignore(x => x.CrudState);
                b.Ignore(x => x.Events);

                b.ToTable("SmsTemplate");
            });

            builder.Entity<App>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.UserId);

                b.Property(x => x.IsActive);
                b.Property(x => x.DisplayName);
                b.Property(x => x.IsLocalAccountEnabled);
                b.Property(x => x.Key);
                b.Property(x => x.WebsiteUrl);
                b.Property(x => x.CreatedAt);
                b.Property(x => x.IsRememberLogInEnabled);
                b.Property(x => x.UsersCount);
                b.Property(x => x.EmailSettings.IsEnabled).HasColumnName("IsEmailEnabled");
                b.Property(x => x.EmailSettings.IsConfirmationRequired).HasColumnName("IsEmailConfirmationRequired");
                b.Property(x => x.EmailSettings.IsPasswordEnabled).HasColumnName("IsEmailPasswordEnabled");
                b.Property(x => x.EmailSettings.IsPasswordlessEnabled).HasColumnName("IsEmailPasswordlessEnabled");
                b.Property(x => x.EmailSettings.IsSearchRelatedProviderEnabled).HasColumnName("IsEmailSearchRelatedProviderEnabled");

                b.Property(x => x.PhoneSettings.IsEnabled).HasColumnName("IsPhoneEnabled");
                b.Property(x => x.PhoneSettings.IsConfirmationRequired).HasColumnName("IsPhoneConfirmationRequired");
                b.Property(x => x.PhoneSettings.IsPasswordEnabled).HasColumnName("IsPhonePasswordEnabled");
                b.Property(x => x.PhoneSettings.IsPasswordlessEnabled).HasColumnName("IsPhonePasswordlessEnabled");

                b.HasMany(x => x.ExternalProviders).WithOne().HasForeignKey(x => x.AppId);
            });

            builder.Entity<AppExternalProvider>(b =>
            {
                b.HasKey(x => x.Id);
                b.HasOne(x => x.ExternalProvider).WithMany().HasForeignKey(x => x.ExternalProviderId);
            });

            builder.Entity<ExternalProvider>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.DisplayName);
                b.Property(x => x.AuthenticationScheme);
            });
        }
    }
}
