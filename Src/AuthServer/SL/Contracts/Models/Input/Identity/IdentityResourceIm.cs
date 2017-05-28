using System.Collections.Generic;

namespace AuthGuard.SL.Contracts.Models.Input.Identity
{
    public class IdentityResourceIm
    {
        public string Description { get; set; }
        public string DisplayName { get; set; }
        public bool Emphasize { get; set; }
        public string Name { get; set; }
        public bool IsRequired { get; set; }
        public bool ShowInDiscoveryDocument { get; set; }
        public bool IsEnabled { get; set; }

        public IEnumerable<IdentityClaimIm> Claims { get; set; }
    }
}