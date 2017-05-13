using System.Linq;
using System.Threading.Tasks;
using AuthGuard.BLL.Domain.Entities;
using DddCore.Contracts.BLL.Domain.Entities.Model;
using DddCore.Contracts.DAL;
using DddCore.Contracts.DAL.DomainStack;
using DddCore.Crosscutting;
using DddCore.DAL.DomainStack.EntityFramework;
using DddCore.DAL.DomainStack.EntityFramework.Context;
using DddCore.DAL.DomainStack.EntityFramework.Mapping;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AuthGuard.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IDataContext, IUnitOfWork
    {
        readonly ConnectionStrings connectionStrings;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IOptions<ConnectionStrings> connectionStrings)
            : base(options)
        {
            this.connectionStrings = connectionStrings.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionStrings.Oltp);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            var modules = AssemblyUtility.GetInstancesOf<IMappingModuleInstaller>().ToArray();
            if (!modules.Any()) return;

            var builder = new DddCoreModelBuilder(modelBuilder);

            foreach (var mappingModuleInstaller in modules)
            {
                mappingModuleInstaller.Install(builder);
            }
        }

        public void SyncEntityState<T>(T entity) where T : class, ICrudState
        {
            Entry(entity).State = CrudStateHelper.ConvertState(entity.CrudState);
        }

        public void Save()
        {
            SaveChanges();
        }

        public async Task SaveAsync()
        {
            await SaveChangesAsync();
        }
    }
}