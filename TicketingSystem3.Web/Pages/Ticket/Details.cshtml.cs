using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TicketingSystem3.Data.Data;
using TicketingSystem3.Data.Models;

namespace TicketingSystem3.Web.Pages.Ticket
{
    [Authorize(Roles = "Admin, Support, Customer")]
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Data.Models.Ticket Ticket { get; set; }
        public IList<Data.Models.Message> Message { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null || _context.Tickets == null)
            {
                return NotFound();
            }

            if (_context.Messages != null)
            {
                Message = await _context.Messages
                .Include(m => m.Ticket)
                .Include(m => m.User).ToListAsync();
            }

            var ticket = await _context.Tickets.FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }
            else
            {
                Ticket = ticket;
            }
            return Page();
        }
    }
}
