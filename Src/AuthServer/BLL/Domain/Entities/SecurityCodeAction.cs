namespace AuthGuard.BLL.Domain.Entities
{
    public enum SecurityCodeAction
    {
        ResetPassword = 1, 
        ConfirmAccount = 2,
        PasswordlessSignUp = 3,
        PasswordlessLogIn = 4,
        TwoFactorVerification = 5
    }
}