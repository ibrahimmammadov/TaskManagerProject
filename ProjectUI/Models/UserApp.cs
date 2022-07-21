using Microsoft.AspNetCore.Identity;

namespace ProjectUI.Models
{
    public class UserApp: IdentityUser
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public UserAddress? UserAddress { get; set; }
        public virtual ICollection<Tasks>? Tasks { get; set; }
    }
}
