namespace AuthGuard.Services.Users.Models.Input
{
    public class ConfirmAccountIm : ConfirmationCodeIm
    {
        public string Provider { get; set; }
    }
}
