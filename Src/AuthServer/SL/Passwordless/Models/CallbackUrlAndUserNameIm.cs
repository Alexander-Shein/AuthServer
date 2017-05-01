using AuthGuard.Api;

namespace AuthGuard.SL.Passwordless.Models
{
    public class CallbackUrlAndUserNameIm : UserNameIm
    {
        public string CallbackUrl { get; set; }
    }
}