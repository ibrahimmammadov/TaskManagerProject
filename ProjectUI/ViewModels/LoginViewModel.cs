using System.ComponentModel.DataAnnotations;

namespace ProjectUI.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "As to:")]
        public string UserType { get; set; }
    }
}
