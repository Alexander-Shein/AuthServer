using System.Collections.Generic;

namespace AuthGuard.SL.Contracts.Models.Input.ApiServices
{
    public class ApiScopeIm
    {
        public string Description { get; set; }
        public string DisplayName { get; set; }
        public bool Emphasize { get; set; }
        public bool IsEnabled { get; set; }
        public string Name { get; set; }
        public bool IsRequired { get; set; }

        public IEnumerable<ApiScopeClaimIm> UserClaims { get; set; }
    }
}