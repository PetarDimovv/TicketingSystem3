﻿using System.ComponentModel.DataAnnotations;

namespace TicketingSystem3.Data.Models.Enums
{
    public enum MessageStatus
    {
        [Display(Name = "Draft")]
        Draft,
        [Display(Name = "Published")]
        Published
    }

    public class UnverifiedMessageStatusPayload
    {
        public MessageStatus Status { get; set; } = MessageStatus.Draft;
    }
}
