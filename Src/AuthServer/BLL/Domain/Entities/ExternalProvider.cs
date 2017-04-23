using System;

namespace AuthGuard.BLL.Domain.Entities
{
    public class ExternalProvider
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string AuthenticationScheme { get; set; }
    }
}