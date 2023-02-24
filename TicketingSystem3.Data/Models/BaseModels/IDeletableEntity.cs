namespace TicketingSystem3.Data.Models.BaseModels
{
    public interface IDeletableEntity
    {
        bool IsDeleted { get; set; }
        DateTime? DeletedOn { get; set; }
    }
}
