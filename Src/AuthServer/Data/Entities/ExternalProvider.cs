using System;

namespace AuthServer.Data.Entities
{
    public class ExternalProvider
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string AuthenticationScheme { get; set; }
    }
}