namespace IdentityServerWithAspNetIdentity.Services.Users.Models.View
{
    public class UserExternalProviderVm
    {
        public string AuthenticationScheme { get; set; }
        public string Key { get; set; }
        public string DisplayName { get; set; }
    }
}
