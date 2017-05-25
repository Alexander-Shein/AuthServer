using System.Threading;
using System.Threading.Tasks;

namespace AuthGuard.BLL.Domain.Entities.Identity.BusinessRules
{
    public interface IIdentityResourceUniqueName
    {
        Task<bool> IsUniqueAsync(string name, CancellationToken cancellationToken);
    }
}