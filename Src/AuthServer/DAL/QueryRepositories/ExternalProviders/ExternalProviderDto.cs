using System;

namespace AuthGuard.DAL.QueryRepositories.ExternalProviders
{
    public class ExternalProviderDto
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string AuthenticationScheme { get; set; }
    }
}