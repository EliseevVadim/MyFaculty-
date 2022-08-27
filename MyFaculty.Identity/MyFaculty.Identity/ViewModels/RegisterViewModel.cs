using System.ComponentModel.DataAnnotations;

namespace MyFaculty.Identity.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Введенные пароли не совпадают")]
        public string ConfirmedPassword { get; set; }
        public string ReturnUrl { get; set; }
        public string ErrorMessage { get; set; }
    }
}
