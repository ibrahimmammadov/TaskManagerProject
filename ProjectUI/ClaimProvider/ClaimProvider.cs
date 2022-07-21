using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using ProjectUI.Models;
using System.Security.Claims;

namespace ProjectUI.ClaimProvider
{
    public class ClaimProvider
    {
        public UserManager<UserApp> userManager { get; set; }

        public ClaimProvider(UserManager<UserApp> userManager)
        {
            this.userManager = userManager;
        }


  
    }
}
