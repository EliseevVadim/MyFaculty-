using System.ComponentModel.DataAnnotations;

namespace MyFaculty.Identity.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
        public string ErrorMessage { get; set; }
    }
}
