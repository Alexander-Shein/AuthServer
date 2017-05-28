using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AuthGuard.BLL.Domain.Entities.Identity.BusinessRules
{
    public interface IIdentityClaimsExistValidator
    {
        Task<bool> AreIdentityClaimsExist(IEnumerable<Guid> ids, CancellationToken cancellationToken);
    }
}