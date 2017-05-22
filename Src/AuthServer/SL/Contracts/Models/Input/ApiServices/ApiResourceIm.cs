using System.Collections.Generic;

namespace AuthGuard.SL.Contracts.Models.Input.ApiServices
{
    public class ApiResourceIm
    {
        public string Description { get; set; }
        public bool IsEnabled { get; set; }
        public string DisplayName { get; set; }
        public string Secret { get; set; }
        public string Name { get; set; }

        public IEnumerable<ApiScopeIm> Scopes { get; set; }
    }
}