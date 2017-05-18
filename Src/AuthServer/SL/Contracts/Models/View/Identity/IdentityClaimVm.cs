using System;
using AuthGuard.SL.Contracts.Models.Input.Identity;

namespace AuthGuard.SL.Contracts.Models.View.Identity
{
    public class IdentityClaimVm : IdentityClaimIm
    {
        public Guid Id { get; set; }
        public bool IsReadOnly { get; set; }
    }
}