using System.Collections.Generic;
using System.Threading.Tasks;
using DddCore.Contracts.DAL.QueryStack;

namespace AuthGuard.DAL.QueryRepositories.ExternalProviders
{
    public interface IExternalProvidersQueryRepository : IQueryRepository
    {
        Task<IEnumerable<ExternalProviderDto>> GetAll();
        Task<IEnumerable<SearchableExternalProviderDto>> GetSearchableAsync();
    }
}