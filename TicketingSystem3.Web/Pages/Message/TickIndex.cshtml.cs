using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TicketingSystem3.Data.Data;

namespace TicketingSystem3.Web.Pages.Message
{
    public class TickIndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public TickIndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Data.Models.Message> Message { get; set; } = default!;

        [BindProperty]
        public long PassedId { get; set; }

        public async Task OnGetAsync(long id)
        {
            PassedId = id;
            if (_context.Messages != null)
            {
                Message = await _context.Messages
                .Include(m => m.Ticket)
                .Include(m => m.User).ToListAsync();
            }
        }
    }
}
