using Microsoft.AspNetCore.Identity;
using ProjectUI.Models;
using ProjectUI.ViewModels;

namespace ProjectUI.Services
{
    public class UserService:BaseService
    {
        private UserApp user;
        public UserService(UserManager<UserApp> _userManager, SignInManager<UserApp> _signInManager, RoleManager<UserRole> _roleManager, IHttpContextAccessor httpContextAccessor) : base(_userManager, _signInManager, _roleManager, httpContextAccessor)
        {
        }

        public bool CheckUserPhoneNumber(string phonenumber)
        {
            return userManager.Users.Any(u => u.PhoneNumber == phonenumber);
        }

        public async Task<IdentityResult> AddNewUser(CompanyViewModel userViewModel)
        {
            user = new UserApp();
            user.Name = userViewModel.Name;
            user.UserName = userViewModel.UserName;
            user.Email = userViewModel.Email;
            user.PhoneNumber = userViewModel.PhoneNumber;
            UserAddress userAddress = new UserAddress { Address1 = userViewModel.Addresss, UserApp = user, UserId = user.Id };
            user.UserAddress = userAddress;
            return await userManager.CreateAsync(user, userViewModel.Password);
        }
         
        public async Task<IdentityResult> AddAdminRole()
        {
            var checkrole = roleManager.RoleExistsAsync("Admin").Result;
            if (!checkrole)
            {
                await roleManager.CreateAsync(new UserRole { Name = "Admin" });
            }
               return await userManager.AddToRoleAsync(user, "Admin");
        }

        public List<UserRole> GetAllUserRole()
        {
            return  roleManager.Roles.ToList();
        }

        public async Task<UserApp> FindByEmailAsync(string email)
        {
           return await userManager.FindByEmailAsync(email);
        }


        public async Task<string> GetUserRoleByType(string usertype)
        {
            var signuserrole = await roleManager.FindByIdAsync(usertype);
            return signuserrole.Name;
        }

        public async Task<string> GetRoleAsync(UserApp user)
        {
            var defaultuserrole = await userManager.GetRolesAsync(user);
            return defaultuserrole.First();
        }
        public async Task<SignInResult> PasswordSignInAsync(UserApp user, string password)
        {
            return await signInManager.PasswordSignInAsync(user, password, false, false);
        }

        public void SignOutAsync()
        {
            signInManager.SignOutAsync();
        }

 

    }
}
