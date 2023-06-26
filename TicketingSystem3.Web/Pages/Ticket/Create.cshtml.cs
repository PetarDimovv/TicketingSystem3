using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using TicketingSystem3.Data.Data;

namespace TicketingSystem3.Web.Pages.Ticket
{
    [Authorize(Roles = "Admin, Support, Customer")]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Data.Models.ApplicationUser> _userManager;
        

        public CreateModel(ApplicationDbContext context, UserManager<Data.Models.ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Data.Models.ViewModels.CreateTicket CreateTicketRequest { get; set; }

        [BindProperty]
        public Data.Models.Ticket Ticket { get; set; }
       
        [BindProperty]
        public long Id { get; set; }


        public IActionResult OnGet(long id)
        {
            if(id == 0)
            {
                return NotFound();
            }
            Id = id;
            //ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id");
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;
            var ticket = new Data.Models.Ticket()
            {
                Title = CreateTicketRequest.Title,
                Description = CreateTicketRequest.Description,
                UserId = userId,
                ProjectId = Id,
                Type = CreateTicketRequest.Type,
                Status = CreateTicketRequest.Status,
                SubmissionDate = CreateTicketRequest.SubmissionDate,
                CreatedOn = DateTime.Now,
            };
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
