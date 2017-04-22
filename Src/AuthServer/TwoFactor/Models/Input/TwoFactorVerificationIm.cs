namespace AuthGuard.TwoFactor.Models.Input
{
    public class TwoFactorVerificationIm
    {
        public string TwoFactorProviderKey { get; set; }
        public string Code { get; set; }
        public bool RememberBrowser { get; set; }
        public bool RememberLogIn { get; set; }
    }
}