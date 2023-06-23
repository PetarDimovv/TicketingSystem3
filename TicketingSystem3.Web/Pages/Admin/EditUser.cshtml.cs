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
        private readonly SignInManager<ApplicationUser> _signInManager;

        public EditUserModel (UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [BindProperty]
        public ApplicationUser User { get; set; }

        [BindProperty]
        public string UserId { get; set; }

        [BindProperty]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public async Task<IActionResult> OnGetAsync(string userId)
        {
            User = await _userManager.FindByIdAsync(userId);

            if (User == null)
            {
                return NotFound();
            }

            UserId = userId;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByIdAsync(UserId);

            if (user == null)
            {
                return NotFound();
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
