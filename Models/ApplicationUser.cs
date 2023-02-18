using Microsoft.AspNetCore.Identity;

namespace TicketingSystem3.Web.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public List<Message>? Messages { get; set; }
        public List<Project>? Projects { get; set; }
        public List<Ticket>? Tickets { get; set; }
    }
}
