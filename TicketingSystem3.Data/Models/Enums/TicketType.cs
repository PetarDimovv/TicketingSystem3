using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TicketingSystem3.Data.Models.Enums
{
    public enum TicketType
    {
        [Display(Name = "BugReport")]
        BugReport,
        [Display(Name = "FeatureRequest")]
        FeatureRequest,
        [Display(Name = "AssistanceRequest")]
        AssistanceRequest,
        [Display(Name = "Other")]
        Other
    }
    public class UnverifiedTicketTypePayload
    {
        public TicketType Types { get; set; } = TicketType.BugReport; 
    }
}
