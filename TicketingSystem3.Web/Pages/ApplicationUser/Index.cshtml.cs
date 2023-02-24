using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TicketingSystem3.Data.Data;

namespace TicketingSystem3.Web.Pages.ApplicationUser
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public IList<Data.Models.ApplicationUser> ApplicationUsers { get; set; } = default!;

        public void OnGet()
        {
        }
    }
}
