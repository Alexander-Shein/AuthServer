using DddCore.Contracts.DAL.QueryStack;
using System.Threading.Tasks;

namespace AuthGuard.DAL.QueryRepositories.Apps
{
    public interface IAppsQueryRepository : IQueryRepository
    {
        Task<bool> IsAppExist(string key);
    }
}