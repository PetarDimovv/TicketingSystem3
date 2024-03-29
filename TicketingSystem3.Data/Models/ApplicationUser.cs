﻿using Microsoft.AspNetCore.Identity;

namespace TicketingSystem3.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool IsApprove { get; set; }
        public List<Message>? Messages { get; set; }
        public List<Project>? Projects { get; set; }
        public List<Ticket>? Tickets { get; set; }
    }
}
