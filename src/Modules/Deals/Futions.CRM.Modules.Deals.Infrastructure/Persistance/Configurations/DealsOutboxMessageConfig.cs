using Futions.CRM.Modules.Deals.Domain.OutboxMessages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Futions.CRM.Modules.Deals.Infrastructure.Persistance.Configurations;
public class DealsOutboxMessageConfig : IEntityTypeConfiguration<DealsOutboxMessage>
{
    public void Configure(EntityTypeBuilder<DealsOutboxMessage> builder)
    {
        builder.Property(o => o.Content)
            .HasColumnType("nvarchar(max)");
    }
}
