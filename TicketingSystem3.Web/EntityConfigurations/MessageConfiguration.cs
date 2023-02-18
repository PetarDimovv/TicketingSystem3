using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketingSystem3.Web.Models;

namespace TicketingSystem.Data.EntityConfigurations
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder
                .HasOne(e => e.User)
                .WithMany(s => s.Messages)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(e => e.Ticket)
                .WithMany(s => s.Messages)
                .HasForeignKey(e => e.TicketId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
