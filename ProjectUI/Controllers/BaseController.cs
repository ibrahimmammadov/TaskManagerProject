using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectUI.Models;
using ProjectUI.Services;
using System.Security.Claims;

namespace ProjectUI.Controllers
{
    public class BaseController : Controller
    {

        protected readonly UserService _userService;

        public BaseController(UserService userService)
        {

            _userService = userService;
        }


        public void AddModelError(IdentityResult result)
        {
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }
        }
    }
}
