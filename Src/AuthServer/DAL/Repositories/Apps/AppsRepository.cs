using System;
using System.Threading.Tasks;
using AuthGuard.BLL.Domain.Entities;
using DddCore.Contracts.Crosscutting.UserContext;
using DddCore.DAL.DomainStack.EntityFramework;
using DddCore.DAL.DomainStack.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace AuthGuard.DAL.Repositories.Apps
{
    public class AppsRepository : Repository<App, Guid>, IAppsRepository
    {

        public AppsRepository(
            IDataContext dataContext,
            IUserContext<Guid> userContext) : base(dataContext, userContext)
        {
        }

        public async Task<App> ReadWithProvidersById(Guid id)
        {
            return await GetDbSet()
                .Include(x => x.ExternalProviders)
                .ThenInclude(x => x.ExternalProvider)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<App> ReadWithProvidersByKey(string key)
        {
            return await GetDbSet()
                .Include(x => x.ExternalProviders)
                .ThenInclude(x => x.ExternalProvider)
                .FirstOrDefaultAsync(x => x.Key == key);
        }
    }
}