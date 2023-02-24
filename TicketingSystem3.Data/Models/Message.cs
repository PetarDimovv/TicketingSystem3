using TicketingSystem3.Data.Models.BaseModels;
using TicketingSystem3.Data.Models.Enums;

namespace TicketingSystem3.Data.Models
{
    public class Message : BaseModel<long>
    {
        public DateTime PublicationDate { get; set; }
        public string UserId { get; set; }
        public ApplicationUser? User { get; set; }
        public long TicketId { get; set; }
        public Ticket? Ticket { get; set; }
        public MessageStatus Status { get; set; }
        public string? Content { get; set; }
        public List<File>? Files { get; set; }
    }
}
