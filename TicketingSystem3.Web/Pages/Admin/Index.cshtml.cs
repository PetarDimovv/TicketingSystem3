using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TicketingSystem3.Data.Data;
using TicketingSystem3.Data.Models;
using TicketingSystem3.Data.Models.ViewModels;

namespace TicketingSystem3.Web.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _dbContext;
        public IndexModel(UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
        }

        public List<ViewUsersModel> Users { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            Users = new List<ViewUsersModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var viewUsersModel = new ViewUsersModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    IsApprove = user.IsApprove,
                    Roles = roles.ToList()
                };

                Users.Add(viewUsersModel);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostUserApprove(string id)
        {
            var userToApprove = await _userManager.FindByIdAsync(id);

            if (userToApprove == null)
            {
                return NotFound();
            }

            userToApprove.IsApprove = true;

            await _dbContext.SaveChangesAsync();

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostUserReject(string id)
        {
            var userToApprove = await _userManager.FindByIdAsync(id);

            if (userToApprove == null)
            {
                return NotFound();
            }

            userToApprove.IsApprove = false;

            await _dbContext.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}