using TicketingSystem3.Data.Models.BaseModels;

namespace TicketingSystem3.Data.Models
{
    public class Project : BaseModel<long>
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }
        public List<Ticket>? Tickets { get; set; }
    }
}
