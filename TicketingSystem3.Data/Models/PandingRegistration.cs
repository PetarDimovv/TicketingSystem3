using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystem3.Data.Models.BaseModels;
using TicketingSystem3.Data.Models.Enums;

namespace TicketingSystem3.Data.Models
{
    public class PandingRegistration : BaseModel<long>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
    }
}
