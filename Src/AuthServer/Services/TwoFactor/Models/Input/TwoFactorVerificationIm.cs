namespace AuthGuard.Services.TwoFactor.Models.Input
{
    public class TwoFactorVerificationIm
    {
        public int Code { get; set; }
        public bool RememberBrowser { get; set; }
        public bool RememberLogIn { get; set; }
    }
}