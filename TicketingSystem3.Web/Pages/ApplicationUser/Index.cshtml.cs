using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TicketingSystem3.Data.Data;

namespace TicketingSystem3.Web.Pages.ApplicationUser
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<Data.Models.ApplicationUser> _userManager;
        //private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        
        public IndexModel(UserManager<Data.Models.ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IList<Data.Models.ApplicationUser> ApplicationUsers { get; set; } = default!;

        [BindProperty]
        public InputModel Input { get; set; }



        public class InputModel
        {

            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }


        }

        public async Task<IActionResult> OnGetAsync(string ID)
        {
            var UsertoEdit = _userManager.Users.FirstOrDefault(u => u.Id == ID);

            if (UsertoEdit == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }



            await LoadAsync(ID);
            return Page();

        }
        private async Task LoadAsync(string ID)
        {


            var UsertoEdit = _userManager.Users.FirstOrDefault(u => u.Id == ID);



            Input = new InputModel
            {
                FirstName = UsertoEdit.FirstName,
                LastName = UsertoEdit.LastName,
                Username = UsertoEdit.UserName,
                Email = UsertoEdit.Email
            };
        }
    }
}
