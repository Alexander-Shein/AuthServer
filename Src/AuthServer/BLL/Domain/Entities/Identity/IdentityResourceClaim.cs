using System;
using DddCore.BLL.Domain.Entities.GuidEntities;

namespace AuthGuard.BLL.Domain.Entities.Identity
{
    public class IdentityResourceClaim : GuidEntityBase
    {
        public Guid IdentityClaimId { get; set; }
        public IdentityClaim IdentityClaim { get; set; }
    }
}