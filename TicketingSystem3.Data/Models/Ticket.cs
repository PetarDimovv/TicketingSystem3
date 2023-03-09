using TicketingSystem3.Data.Models.BaseModels;
using TicketingSystem3.Data.Models.Enums;

namespace TicketingSystem3.Data.Models
{
    public class Ticket : BaseModel<long>
    {
        public DateTime SubmissionDate { get; set; }
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }
        public long ProjectId { get; set; }
        public Project? Project { get; set; }
        public TicketType Type { get; set; }
        public TicketStatus Status { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public List<File>? Files { get; set; }
        public List<Message>? Messages { get; set; }
    }
}
