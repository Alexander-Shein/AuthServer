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
        }
    }
}
