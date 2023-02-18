using TicketingSystem.Data.Models.BaseModels;

namespace TicketingSystem3.Web.Models
{
    public class File : BaseModel<long>
    {
        public string? FileName { get; set; }
        public byte[]? Content { get; set; }
        public long MessageId { get; set; }
        public Message? Message { get; set; }
        public long TicketId { get; set; }
        public Ticket? Ticket { get; set; }
    }
}
