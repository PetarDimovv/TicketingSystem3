using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TicketingSystem3.Data.Data;
using TicketingSystem3.Data.Models;

namespace TicketingSystem3.Web.Pages.Ticket
{
    [Authorize(Roles = "Admin, Support, Customer")]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EditModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Data.Models.Ticket Ticket { get; set; } = default!;

        [BindProperty]
        public DateTime CreatedOn { get; set; }

        [BindProperty]
        public long NewProjectId { get; set; }

        [BindProperty]
        public string UserId { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null || _context.Tickets == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets.FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            CreatedOn = ticket.CreatedOn;
            NewProjectId = ticket.ProjectId;
            UserId = ticket.UserId;
            Ticket = ticket;
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

            var user = await _userManager.FindByIdAsync(UserId);
            _context.Attach(Ticket).State = EntityState.Modified;
            Ticket.ModifiedOn = DateTime.Now;
            Ticket.ProjectId = NewProjectId;
            Ticket.UserId = user.Id;
            Ticket.CreatedOn = CreatedOn;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(Ticket.Id))
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

        private bool TicketExists(long id)
        {
            return _context.Tickets.Any(e => e.Id == id);
        }
    }
}
