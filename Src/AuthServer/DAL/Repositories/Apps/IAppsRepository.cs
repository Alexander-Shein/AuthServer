using System;
using System.Threading.Tasks;
using AuthGuard.BLL.Domain.Entities;
using DddCore.Contracts.DAL.DomainStack;

namespace AuthGuard.DAL.Repositories.Apps
{
    public interface IAppsRepository : IRepository<App, Guid>
    {
        Task<App> ReadWithProvidersById(Guid id);
        Task<App> ReadWithProvidersByKey(string key);
    }
}