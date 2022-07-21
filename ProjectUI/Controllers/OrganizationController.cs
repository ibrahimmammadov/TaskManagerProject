using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectUI.Models;
using ProjectUI.Services;
using ProjectUI.ViewModels;

namespace ProjectUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrganizationController : BaseController
    {
        protected readonly OrganizationService _organizationService;
        public OrganizationController(OrganizationService organizationService, UserService _userService) : base( _userService)
        {
            _organizationService = organizationService;
        }
        public async Task<IActionResult> Index()
        {
            var basicusers = await _organizationService.GetBasicUser();
            return View(basicusers);
        }

        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _organizationService.SaveNewUser(userViewModel);
                if (result.Succeeded)
                {
                    var role = await _organizationService.AddBasicUserRole();

                    if (role.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        AddModelError(role);
                    }
                }
                else
                {
                    AddModelError(result);
                }
            }

            return View(userViewModel);
        }

      

    }
}
