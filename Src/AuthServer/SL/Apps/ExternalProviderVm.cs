using System;

namespace AuthGuard.SL.Apps
{
    public class ExternalProviderVm
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string AuthenticationScheme { get; set; }
    }
}