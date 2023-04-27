using System.ComponentModel.DataAnnotations;

namespace MyFaculty.Identity.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string ReturnUrl { get; set; }
        public string ErrorMessage { get; set; }
    }
}
