using System;

namespace AuthGuard.SL.Contracts.Models.Input.Identity
{
    public class IdentityResourceClaimIm : IdentityClaimIm
    {
        public Guid Id { get; set; }
    }
}