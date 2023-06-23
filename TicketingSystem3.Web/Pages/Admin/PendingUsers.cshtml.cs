using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TicketingSystem3.Data.Models;
using TicketingSystem3.Data.Models.ViewModels;

namespace TicketingSystem3.Web.Pages.Admin
{
    public class PendingUsersModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public PendingUsersModel(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
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
                    Roles = roles.ToList()
                };

                Users.Add(viewUsersModel);
            }

            return Page();
        }
    }
}
