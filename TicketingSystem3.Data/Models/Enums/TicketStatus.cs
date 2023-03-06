using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TicketingSystem3.Data.Models.Enums
{
    public enum TicketStatus
    {
        [Display(Name = "Draft")]
        Draft,
        [Display(Name = "New")]
        New,
        [Display(Name = "WorkedOn")]
        WorkedOn,
        [Display(Name = "Completed")]
        Completed
    }
    public class UnverifiedTicketStatusPayload
    {
        public TicketStatus Status { get; set; } = TicketStatus.Draft;
    }
}
