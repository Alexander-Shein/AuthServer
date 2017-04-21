using System.ComponentModel.DataAnnotations;

namespace AuthGuard.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
