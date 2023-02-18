﻿using Microsoft.AspNetCore.Identity;

namespace TicketingSystem3.Web.Data.CustomRoles
{
    public class CustomRoleManager : ICustomRoleManager
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public CustomRoleManager(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<bool> RoleExistsAsync(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }

        public async Task<IdentityResult> CreateRoleAsync(string roleName)
        {
            var role = new IdentityRole(roleName);
            return await _roleManager.CreateAsync(role);
        }

        public async Task<IdentityResult> DeleteRoleAsync(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            return await _roleManager.DeleteAsync(role);
        }
    }
}
