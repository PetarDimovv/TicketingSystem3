using System.ComponentModel.DataAnnotations;

namespace TicketingSystem3.Data.Models.BaseModels
{
    public abstract class BaseModel<TKey> : IAuditInfo
        where TKey : IComparable<TKey>
    {
        [Key]
        [Required]
        public TKey Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
