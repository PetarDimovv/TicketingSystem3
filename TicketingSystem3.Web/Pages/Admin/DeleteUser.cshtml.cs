using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TicketingSystem3.Data.Models;

namespace TicketingSystem3.Web.Pages.Admin
{
    public class DeleteUserModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public DeleteUserModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public string PassedId { get; set; }


        public IActionResult OnGet(string userId)
        {
            PassedId = userId;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.FindByIdAsync(PassedId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{PassedId}'.");
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                var result = await _userManager.RemoveFromRoleAsync(user, role);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, $"Failed to remove user from role '{role}'.");
                    return Page();
                }
            }

            var deleteResult = await _userManager.DeleteAsync(user);
            if (!deleteResult.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Failed to delete the user.");
                return Page();
            }

            return Redirect("/Admin/Index");
        }
    }
}
