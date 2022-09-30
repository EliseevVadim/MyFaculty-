using System.ComponentModel.DataAnnotations;

namespace MyFaculty.Identity.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Введенные пароли не совпадают")]
        public string ConfirmedPassword { get; set; }
        public string Code { get; set; }
        public string ErrorMessage { get; set; }
    }
}
