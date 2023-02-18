﻿using Microsoft.AspNetCore.Identity;

namespace TicketingSystem3.Web.Data.CustomRoles
{
    public interface ICustomRoleManager
    {
        Task<bool> RoleExistsAsync(string roleName);
        Task<IdentityResult> CreateRoleAsync(string roleName);
        Task<IdentityResult> DeleteRoleAsync(string roleName);
    }
}
