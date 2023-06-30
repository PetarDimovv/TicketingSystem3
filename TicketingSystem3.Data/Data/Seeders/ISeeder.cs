namespace TicketingSystem3.Data.Data.Seeders
{
    public interface ISeeder
    {
        public Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider);
    }
}
