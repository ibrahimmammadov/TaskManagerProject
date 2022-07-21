using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectUI.Models;
using ProjectUI.ViewModels;

namespace ProjectUI.Services
{
    public class MemberService : BaseService
    {
        private readonly AppDbContext _context;
        public MemberService(AppDbContext context, UserManager<UserApp> _userManager, SignInManager<UserApp> _signInManager, RoleManager<UserRole> _roleManager, IHttpContextAccessor httpContextAccessor) : base(_userManager, _signInManager, _roleManager, httpContextAccessor)
        {
            _context = context;
        }

        public async Task<List<Tasks>> GetCurrentUserTasks()
        {
            return await _context.Tasks.Include(x => x.UserApps).Where(tasks => tasks.UserApps.Select(x => x.Id).Contains(CurrentUserId)).ToListAsync();
        }

        public async Task<List<Tasks>> GetAllTasks()
        {
            return await _context.Tasks.Where(x => x.Status == 1).ToListAsync();
        }

        public async Task<UserApp> UserInform()
        {
            return await userManager.FindByIdAsync(CurrentUserId);
        }

        public async Task<IList<UserApp>> GetBasicUser()
        {
            return await userManager.GetUsersInRoleAsync("BasicUser");
        }

        public async Task<int> SaveNewTask (TaskViewModel taskView)
        {
            Tasks tasks = new Tasks();
            tasks.Tittle = taskView.Tittle;
            tasks.Description = taskView.Description;
            tasks.Status = 1;
            tasks.Deadline = taskView.Deadline;

            List<UserApp> users = new List<UserApp>();
            foreach (var item in taskView.UserId)
            {
                users.Add(await userManager.FindByIdAsync(item));
                tasks.UserApps = users;
            }
            _context.Tasks.Add(tasks);
            return await _context.SaveChangesAsync();
        }
    }
}
