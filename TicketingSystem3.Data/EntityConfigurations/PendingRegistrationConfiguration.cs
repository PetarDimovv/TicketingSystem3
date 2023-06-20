using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystem3.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace TicketingSystem3.Data.EntityConfigurations
{
    public class PendingRegistrationConfiguration : IEntityTypeConfiguration<PendingRegistration>
    {
        public void Configure(EntityTypeBuilder<PendingRegistration> builder)
        {
            builder.HasKey(pr => pr.Id);
        }
    }
}
