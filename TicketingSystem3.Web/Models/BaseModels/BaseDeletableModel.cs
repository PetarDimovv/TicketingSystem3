namespace TicketingSystem.Data.Models.BaseModels
{
    public abstract class BaseDeletableModel<TKey> : BaseModel<TKey>, IDeletableEntity
         where TKey : IComparable<TKey>
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
