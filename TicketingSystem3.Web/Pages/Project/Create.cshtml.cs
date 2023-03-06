using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Xml.Linq;
using TicketingSystem3.Data.Data;

namespace TicketingSystem3.Web.Pages.Project
{
    [Authorize(Roles = "Admin, Customer")]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Data.Models.ApplicationUser> _userManager;

        public CreateModel(ApplicationDbContext context, UserManager<Data.Models.ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Data.Models.Project Project { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;
            var project = new Data.Models.Project()
            {
                
            };
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
