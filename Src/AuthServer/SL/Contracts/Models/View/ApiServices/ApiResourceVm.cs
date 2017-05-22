using System;
using System.Collections.Generic;

namespace AuthGuard.SL.Contracts.Models.View.ApiServices
{
    public class ApiResourceVm
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public bool IsEnabled { get; set; }
        public string DisplayName { get; set; }
        public string Secret { get; set; }
        public string Name { get; set; }

        public IEnumerable<ApiScopeVm> Scopes { get; set; }
    }
}