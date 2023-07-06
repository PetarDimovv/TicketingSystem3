using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TicketingSystem3.Data.Models;

namespace TicketingSystem3.Data.Data.Seeders
{
    public class AdminSeeder : ISeeder
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserStore<ApplicationUser> _userStore;

        public AdminSeeder(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, 
            IUserStore<ApplicationUser> userStore)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userStore = userStore;
        }

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var user = new ApplicationUser
            {
                FirstName = "Admin",
                LastName = "Example",
                UserName = "AdminExample",
                Email = "admin@gmail.com",
                IsApprove = true,
            };

            await _userStore.SetUserNameAsync(user, user.UserName, CancellationToken.None);
            var result = await _userManager.CreateAsync(user, "Password123!");
            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync("Admin"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                }

                if (!await _roleManager.RoleExistsAsync("Support"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("Support"));
                }

                if (!await _roleManager.RoleExistsAsync("Customer"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("Customer"));
                }
                await _userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}
