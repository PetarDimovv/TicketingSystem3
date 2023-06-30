using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TicketingSystem3.Data.Data;

namespace TicketingSystem3.Web.Pages.Message
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
        public long PassedId { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Data.Models.ViewModels.CreateMessage CreateMessageRequest { get; set; }

        [BindProperty]
        public Data.Models.Message Message { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(long id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            PassedId = id;
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;
            var message = new Data.Models.Message()
            {
                Content = CreateMessageRequest.Content,
                Status = CreateMessageRequest.Status,
                UserId = userId,
                TicketId = PassedId,
                PublicationDate = CreateMessageRequest.PublicationDate,
                CreatedOn = DateTime.Now,
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
