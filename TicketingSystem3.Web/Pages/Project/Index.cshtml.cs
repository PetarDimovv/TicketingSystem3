using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using TicketingSystem3.Data.Data;

namespace TicketingSystem3.Web.Pages.Project
{
    [Authorize(Roles = "Admin, Support, Customer")]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Data.Models.Project> Project { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Projects != null)
            {
                Project = await _context.Projects
                .Include(p => p.User).ToListAsync();
            }
        }
    }
}
