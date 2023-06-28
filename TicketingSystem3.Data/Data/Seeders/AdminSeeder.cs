using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystem3.Data.Models;

namespace TicketingSystem3.Data.Data.Seeders
{
    public class AdminSeeder : ISeeder
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminSeeder(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {


            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            
            var user = new ApplicationUser
            {
                FirstName = "Admin",
                LastName = "Example",
                UserName = "AdminExample",
                Email = "admin@gmail.com",
                IsApprove = true,
            };


            await _userManager.CreateAsync(user, "Password123!");


            await _userManager.AddToRoleAsync(user, "Admin");

        }
    }
}
