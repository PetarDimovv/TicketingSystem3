using TicketingSystem3.Data.Models.Enums;

namespace TicketingSystem3.Data.Models.ViewModels
{
    public class CreateMessage
    {
        public string? Content { get; set; }
        public DateTime PublicationDate { get; set; }
        public long TicketId { get; set; }
        public MessageStatus Status { get; set; }
    }
}
