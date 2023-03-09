using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystem3.Data.Models.Enums;

namespace TicketingSystem3.Data.Models.ViewModels
{
    public class CreateTicket
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime SubmissionDate { get; set; }
        public long ProjectId { get; set; }
        public TicketType Type { get; set; }
        public TicketStatus Status { get; set; }
    }
}
