using ProjectUI.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjectUI.ViewModels
{
    public class TaskViewModel
    {
        [Required]
        [Display(Name = "Tittle")]
        public string Tittle { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Deadline")]
        public DateTime? Deadline { get; set; }

        public List<UserApp>? UserApps { get; set; }

        [Required]
        [Display(Name = "Assign to:")]
        public List<string> UserId { get; set; }
    }
}
