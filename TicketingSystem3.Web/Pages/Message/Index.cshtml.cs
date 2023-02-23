using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TicketingSystem3.Web.Data;
using TicketingSystem3.Web.Models;

namespace TicketingSystem3.Web.Pages.Message
{
    [Authorize(Roles = "Admin, Support, Customer")]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Models.Message> Message { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Messages != null)
            {
                Message = await _context.Messages
                .Include(m => m.Ticket)
                .Include(m => m.User).ToListAsync();
            }
        }
    }
}
