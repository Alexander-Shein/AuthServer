using AuthGuard.SL.Contracts.Models.Input.ApiServices;

namespace AuthGuard.SL.Contracts.Models.View.ApiServices
{
    public class ApiScopeClaimVm : ApiScopeClaimIm
    {
        public string Type { get; set; }
        public string Description { get; set; }
    }
}