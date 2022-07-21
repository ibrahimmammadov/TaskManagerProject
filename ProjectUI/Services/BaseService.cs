using Microsoft.AspNetCore.Identity;
using ProjectUI.Models;
using System.Security.Claims;

namespace ProjectUI.Services
{
    public class BaseService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        protected UserManager<UserApp> userManager { get; }
        protected SignInManager<UserApp> signInManager { get; }

        protected RoleManager<UserRole> roleManager { get; }
        protected string CurrentUserId => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        public BaseService(UserManager<UserApp> _userManager, SignInManager<UserApp> _signInManager, RoleManager<UserRole> _roleManager, IHttpContextAccessor httpContextAccessor)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            roleManager = _roleManager;
            _httpContextAccessor = httpContextAccessor;
        }

        
    }
}
