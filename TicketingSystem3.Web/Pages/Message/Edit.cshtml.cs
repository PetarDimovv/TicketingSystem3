using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TicketingSystem3.Data.Data;
using TicketingSystem3.Data.Models;

namespace TicketingSystem3.Web.Pages.Message
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
        public DateTime CreatedOn { get; set; }

        [BindProperty]
        public Data.Models.Message Message { get; set; } = default!;

        [BindProperty]
        public long NewTicketId { get; set; }

        [BindProperty]
        public string UserId { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null || _context.Messages == null)
            {
                return NotFound();
            }

            var message = await _context.Messages.FirstOrDefaultAsync(m => m.Id == id);
            if (message == null)
            {
                return NotFound();
            }
            CreatedOn = message.CreatedOn;
            NewTicketId = message.TicketId;
            UserId = message.UserId;
            Message = message;

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
            _context.Attach(Message).State = EntityState.Modified;
            Message.ModifiedOn = DateTime.Now;
            Message.TicketId = NewTicketId;
            Message.UserId = user.Id;
            Message.CreatedOn = CreatedOn;

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
