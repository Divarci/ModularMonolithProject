using Futions.CRM.Modules.Deals.Domain.InboxMessages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Futions.CRM.Modules.Deals.Infrastructure.Persistance.Configurations;
public class DealsInboxMessageConfig : IEntityTypeConfiguration<DealsInboxMessage>
{
    public void Configure(EntityTypeBuilder<DealsInboxMessage> builder)
    {
        builder.Property(o => o.Content)
            .HasColumnType("nvarchar(max)");
    }
}
