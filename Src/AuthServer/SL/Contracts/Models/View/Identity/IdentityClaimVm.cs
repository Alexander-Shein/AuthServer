using AuthGuard.SL.Contracts.Models.Input.Identity;

namespace AuthGuard.SL.Contracts.Models.View.Identity
{
    public class IdentityClaimVm : IdentityClaimIm
    {
        public string Type { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsReadOnly { get; set; }
    }
}