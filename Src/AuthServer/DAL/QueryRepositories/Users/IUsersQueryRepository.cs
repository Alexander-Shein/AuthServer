using System.Threading.Tasks;
using DddCore.Contracts.DAL.QueryStack;

namespace AuthGuard.DAL.QueryRepositories.Users
{
    public interface IUsersQueryRepository : IQueryRepository
    {
        Task<bool> IsUserWithEmailExist(string email);
        Task<bool> IsUserWithPhoneExist(string phone);
    }
}