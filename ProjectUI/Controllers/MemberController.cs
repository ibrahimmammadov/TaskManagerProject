using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectUI.Models;
using ProjectUI.Services;
using ProjectUI.ViewModels;
using System.Security.Claims;

namespace ProjectUI.Controllers
{
    [Authorize(Roles = "Admin,BasicUser")]
    public class MemberController : BaseController
    {
        private readonly AppDbContext _context;
        protected readonly MemberService _memberService;
        public MemberController(MemberService memberService,UserService _userService,AppDbContext dbContext) : base( _userService)
        {
            _context = dbContext;
            _memberService = memberService;
        }

        public async Task<IActionResult> Index()
        {
            var getCurrentUserTasks = await _memberService.GetCurrentUserTasks();
            return View(getCurrentUserTasks);
        }

        public async Task<IActionResult> AllTask()
        {
            var alltask = await _memberService.GetAllTasks();
            return View(alltask);
        }

        public async Task<IActionResult> AddNewTask()
        {
            var basicusers = await _memberService.GetBasicUser();
            ViewBag.users = new SelectList(basicusers,"Id","Name");
            return View();
        }

        public async Task<IActionResult> UserInform()
        {
            var currentuser = await _memberService.UserInform();
            return View(currentuser);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewTask(TaskViewModel taskView)
        {
            var basicusers = await _memberService.GetBasicUser();
            ViewBag.users = new SelectList(basicusers, "Id", "Name");
            if (ModelState.IsValid)
            {
                await _memberService.SaveNewTask(taskView);
                return RedirectToAction("Index");
            }

            return View(taskView);
        }
    }
}
