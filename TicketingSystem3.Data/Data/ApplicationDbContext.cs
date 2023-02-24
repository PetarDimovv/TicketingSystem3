using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TicketingSystem3.Data.Models;
using TicketingSystem3.Data.Models.BaseModels;

namespace TicketingSystem3.Data.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private static readonly MethodInfo SetIsDeletedQueryFilterMethod =
            typeof(ApplicationDbContext).GetMethod(
                nameof(SetIsDeletedQueryFilter),
                BindingFlags.NonPublic | BindingFlags.Static);
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketingSystem3.Data.Models.File> Files { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Needed for Identity models configuration
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            var entityTypes = builder.Model.GetEntityTypes().ToList();

            // Set global query filter for not deleted entities only
            var deletableEntityTypes = entityTypes
                .Where(et => et.ClrType != null && typeof(IDeletableEntity).IsAssignableFrom(et.ClrType));
            foreach (var deletableEntityType in deletableEntityTypes)
            {
                var method = SetIsDeletedQueryFilterMethod.MakeGenericMethod(deletableEntityType.ClrType);
                method.Invoke(null, new object[] { builder });
            }

            // Disable cascade delete
            var foreignKeys = entityTypes
                .SelectMany(e => e.GetForeignKeys().Where(f => f.DeleteBehavior == DeleteBehavior.Cascade));
            foreach (var foreignKey in foreignKeys)
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        public override int SaveChanges() => this.SaveChanges(true);

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            var now = DateTime.Now;
            ApplyAuditInfoRules(now);
            ApplySoftDeleteInfoRules(now);
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
           SaveChangesAsync(true, cancellationToken);

        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            var now = DateTime.Now;
            ApplyAuditInfoRules(now);
            ApplySoftDeleteInfoRules(now);
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void ApplyAuditInfoRules(DateTime now)
        {
            var changedEntries = ChangeTracker
                .Entries()
                .Where(e =>
                    e.Entity is IAuditInfo &&
                    (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in changedEntries)
            {
                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default)
                {
                    entity.CreatedOn = now;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entity.ModifiedOn = now;
                }
            }
        }

        private void ApplySoftDeleteInfoRules(DateTime now)
        {
            var deletedEntries = ChangeTracker
                .Entries()
                .Where(e =>
                    e.Entity is IDeletableEntity &&
                    (e.State == EntityState.Deleted));

            foreach (var entry in deletedEntries)
            {
                var entity = (IDeletableEntity)entry.Entity;

                entity.DeletedOn = now;
                entity.IsDeleted = true;
                entry.State = EntityState.Modified;
            }
        }

        private static void SetIsDeletedQueryFilter<T>(ModelBuilder builder)
           where T : class, IDeletableEntity
        {
            builder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
        }
    }
}