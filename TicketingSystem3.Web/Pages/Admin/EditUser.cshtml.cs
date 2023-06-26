using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using TicketingSystem3.Data.Models;

namespace TicketingSystem3.Web.Pages.Admin
{
    public class EditUserModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public EditUserModel (UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [BindProperty]
        public ApplicationUser User { get; set; }

        [BindProperty]
        public string UserId { get; set; }

        [BindProperty]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [BindProperty]
        public string CurrentRole { get; set; }

        public async Task<IActionResult> OnGetAsync(string userId)
        {
            User = await _userManager.FindByIdAsync(userId);


            if (User == null)
            {
                return NotFound();
            }
            var userRoles = _userManager.GetRolesAsync(User).Result;
            CurrentRole = userRoles[0];

            UserId = userId;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string newRole)
        {
            var user = await _userManager.FindByIdAsync(UserId);

            if (user == null)
            {
                return NotFound();
            }
            var currentRoles = _userManager.GetRolesAsync(user).Result;
            var roleResult = _userManager.RemoveFromRolesAsync(user, currentRoles).Result;

            roleResult = _userManager.AddToRoleAsync(user, newRole).Result;

            if (!roleResult.Succeeded)
            {
                ModelState.AddModelError("", "Failed to change user role.");
                return Page();
            }
            user.FirstName = User.FirstName;
            user.LastName = User.LastName;
            user.Email = User.Email;

            if (!string.IsNullOrEmpty(Password))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, Password);

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Failed to change user password.");
                    return Page();
                }
            }

            var updateResult = await _userManager.UpdateAsync(user);

            if (!updateResult.Succeeded)
            {
                ModelState.AddModelError("", "Failed to update user data.");
                return Page();
            }

            //await _signInManager.RefreshSignInAsync(user);

            return RedirectToPage("Index");
        }
    }
}
