using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TicketingSystem3.Web.Data;
using TicketingSystem3.Web.Models;

namespace TicketingSystem3.Web.Pages.Message
{
    [Authorize(Roles = "Admin, Support, Customer")]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel (ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Message Message { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null || _context.Messages == null)
            {
                return NotFound();
            }

            var message =  await _context.Messages.FirstOrDefaultAsync(m => m.Id == id);
            if (message == null)
            {
                return NotFound();
            }
            Message = message;
           ViewData["TicketId"] = new SelectList(_context.Tickets, "Id", "Id");
           ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Message).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageExists(Message.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MessageExists(long id)
        {
          return _context.Messages.Any(e => e.Id == id);
        }
    }
}
