using AuthGuard.BLL.Domain.Entities.Identity;
using DddCore.BLL.Domain.Entities.GuidEntities;

namespace AuthGuard.BLL.Domain.Entities.ApiResources
{
    public class ApiScopeClaim : GuidEntityBase
    {
        public IdentityClaim IdentityClaim { get; set; }
    }
}