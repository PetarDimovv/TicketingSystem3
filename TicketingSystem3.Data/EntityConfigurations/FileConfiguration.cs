using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using File = TicketingSystem3.Data.Models.File;

namespace TicketingSystem3.Data.EntityConfigurations
{
    public class FileConfiguration : IEntityTypeConfiguration<File>
    {
        public void Configure(EntityTypeBuilder<File> builder)
        {
            builder
                .HasOne(e => e.Message)
                .WithMany(s => s.Files)
                .HasForeignKey(e => e.MessageId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(e => e.Ticket)
                .WithMany(s => s.Files)
                .HasForeignKey(e => e.TicketId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
