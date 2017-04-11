
namespace AuthServer.Services.Users.Models.Input
{
    public class UserIm
    {
        public string Email { get; set; }
        public string EmailCode { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumberCode { get; set; }
        public bool? IsTwoFactorEnabled { get; set; }
    }
}