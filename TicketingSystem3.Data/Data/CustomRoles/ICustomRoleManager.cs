using Microsoft.AspNetCore.Identity;

namespace TicketingSystem3.Data.Data.CustomRoles
{
    public interface ICustomRoleManager
    {
        Task<bool> RoleExistsAsync(string roleName);
        Task<IdentityResult> CreateRoleAsync(string roleName);
    }
}
