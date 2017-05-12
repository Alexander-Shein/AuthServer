
namespace AuthGuard.SL.Users.Models.Input
{
    public class UserIm
    {
        public string Email { get; set; }
        public int? EmailCode { get; set; }
        public string PhoneNumber { get; set; }
        public int? PhoneNumberCode { get; set; }
        public bool? IsTwoFactorEnabled { get; set; }
    }
}