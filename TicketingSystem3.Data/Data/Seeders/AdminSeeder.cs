using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TicketingSystem3.Data.Data.CustomRoles;
using TicketingSystem3.Data.Models;

namespace TicketingSystem3.Data.Data.Seeders
{
    public class AdminSeeder : ISeeder
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICustomRoleManager _customRoleManager;
        private readonly IUserStore<ApplicationUser> _userStore;

        public AdminSeeder(UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            ICustomRoleManager customRoleManager)
        {
            _userManager = userManager;
            _userStore = userStore;
            _customRoleManager = customRoleManager;
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
                if (!await _customRoleManager.RoleExistsAsync("Admin"))
                {
                    await _customRoleManager.CreateRoleAsync("Admin");
                }

                if (!await _customRoleManager.RoleExistsAsync("Support"))
                {
                    await _customRoleManager.CreateRoleAsync("Support");
                }

                if (!await _customRoleManager.RoleExistsAsync("Customer"))
                {
                    await _customRoleManager.CreateRoleAsync("Customer");
                }

                await _userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}
