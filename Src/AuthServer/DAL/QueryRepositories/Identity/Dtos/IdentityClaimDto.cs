using System;

namespace AuthGuard.DAL.QueryRepositories.Identity.Dtos
{
    public class IdentityClaimDto
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public bool IsReadOnly { get; set; }
    }
}