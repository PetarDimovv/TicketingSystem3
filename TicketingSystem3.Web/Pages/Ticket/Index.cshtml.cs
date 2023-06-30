using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TicketingSystem3.Data.Data;

namespace TicketingSystem3.Web.Pages.Ticket
{
    [Authorize(Roles = "Admin, Support, Customer")]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Data.Models.Ticket> Ticket { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Tickets != null)
            {
                Ticket = await _context.Tickets
                .Include(t => t.Project)
                .Include(t => t.User).ToListAsync();
            }
        }
    }
}
