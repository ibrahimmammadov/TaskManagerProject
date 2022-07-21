using System.ComponentModel.DataAnnotations;

namespace ProjectUI.ViewModels
{
    public class CompanyViewModel
    {
        [Required]
        [Display(Name = "Organization Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Display(Name = "Tel Number:")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(18, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        public string Addresss { get; set; }

    }
}
