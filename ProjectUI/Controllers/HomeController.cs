using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectUI.Models;
using ProjectUI.Services;
using ProjectUI.ViewModels;
using System.Diagnostics;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace ProjectUI.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
       
        public HomeController(UserService _userService, ILogger<HomeController> logger) :base(_userService)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
          
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(CompanyViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                if (_userService.CheckUserPhoneNumber(userViewModel.PhoneNumber))
                {
                    ModelState.AddModelError("", "Phone number is registered.");
                    return View(userViewModel);
                }
                var result = await _userService.AddNewUser(userViewModel);
                if ( result.Succeeded)
                {
                  var role = await _userService.AddAdminRole();

                    if (role.Succeeded)
                    {
                        return RedirectToAction("SignIn");
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


        public IActionResult SignIn()
        {
            ViewBag.usertype = new SelectList(_userService.GetAllUserRole(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(LoginViewModel userlogin)
        {
            ViewBag.usertype = new SelectList(_userService.GetAllUserRole(), "Id", "Name");
            if (ModelState.IsValid)
            {
                UserApp user = await _userService.FindByEmailAsync(userlogin.Email);
                if (user != null)
                {
                    var name = await _userService.GetUserRoleByType(userlogin.UserType);
                    var defaultuserrole = await _userService.GetRoleAsync(user);
                    _userService.SignOutAsync();

                    SignInResult result = await _userService.PasswordSignInAsync(user, userlogin.Password);

                    if (result.Succeeded)
                    {
                        if (name == "Admin"&&name ==defaultuserrole)
                        {
                            return RedirectToAction("Index", "Organization");
                        }
                        if (name == "BasicUser" && name == defaultuserrole)
                        {
                            return RedirectToAction("Index", "Member");
                        }
                        else
                        {
                            ModelState.AddModelError("", "No access");

                        }
                    }
                    else
                    {
                            ModelState.AddModelError("", "Email or Password is incorrect");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "No user found");
                }
            }

            return View(userlogin);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Logout()
        {
            _userService.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}