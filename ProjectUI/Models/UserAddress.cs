using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectUI.Models
{
    public class UserAddress
    {
        [ForeignKey("UserApp")]
        public string? Id { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? UserId { get; set; }
        public  UserApp? UserApp { get; set; }

    }
}
