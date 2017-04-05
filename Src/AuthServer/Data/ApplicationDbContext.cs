using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using IdentityServerWithAspNetIdentity.Models;
using AuthServer.Data.Entities;

namespace IdentityServerWithAspNetIdentity.Data
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
                b.Property(x => x.FirstName);
                b.Property(x => x.LastName);
                b.Property(x => x.CompanyName);
                b.Property(x => x.FromEmail);
                b.Property(x => x.Subject);
                b.Property(x => x.Message);

                b.ToTable("Email");
            });
        }
    }
}
