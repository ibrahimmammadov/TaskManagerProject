using Microsoft.AspNetCore.Identity;
using ProjectUI.Models;
using ProjectUI.ViewModels;

namespace ProjectUI.Services
{
    public class OrganizationService : BaseService
    {
        public OrganizationService(UserManager<UserApp> _userManager, SignInManager<UserApp> _signInManager, RoleManager<UserRole> _roleManager, IHttpContextAccessor httpContextAccessor) : base(_userManager, _signInManager, _roleManager, httpContextAccessor)
        {
        }
        private UserApp user;

        public async Task<IList<UserApp>> GetBasicUser()
        {
            return await userManager.GetUsersInRoleAsync("BasicUser");
        }

        public async Task<IdentityResult> SaveNewUser(UserViewModel userViewModel)
        {
            user = new UserApp();
            user.Name = userViewModel.Name;
            user.Surname = userViewModel.Surname;
            user.UserName = userViewModel.UserName;
            user.Email = userViewModel.Email;
            return await userManager.CreateAsync(user, userViewModel.Password);
        }

        public async Task<IdentityResult> AddBasicUserRole()
        {
            var checkrole = await roleManager.RoleExistsAsync("BasicUser");
            if (!checkrole)
            {
                await roleManager.CreateAsync(new UserRole { Name = "BasicUser" });
            }
            return await userManager.AddToRoleAsync(user, "BasicUser");
        }
    }
}
