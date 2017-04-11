using System.Collections.Generic;

namespace IdentityServerWithAspNetIdentity.Services.Users.Models.View
{
    public class UserVm
    {
        public string Email { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsPhoneNumberConfirmed { get; set; }
        public bool IsTwoFactorEnabled { get; set; }
        public bool HasPassword { get; set; }

        public IEnumerable<UserExternalProviderVm> ExternalProviders { get; set; }
    }
}
