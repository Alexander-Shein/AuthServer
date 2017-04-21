using AuthGuard.Data.Entities;
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
                b.Property(x => x.Subject);
                b.Property(x => x.ToEmail);
                b.Property(x => x.CreatedAt);

                b.ToTable("Email");
            });

            builder.Entity<EmailTemplate>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.SubjectTemplate);
                b.Property(x => x.BodyTemplate);
                b.Property(x => x.EmailBodyFormat).HasColumnName("EmailBodyFormatId");
                b.HasMany(x => x.EmailTemplateAttachments).WithOne().HasForeignKey(c => c.EmailTemplateId);

                b.ToTable("EmailTemplate");
            });

            builder.Entity<EmailTemplateAttachment>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.FileName);

                b.ToTable("EmailTemplateAttachment");
            });
        }
    }
}
