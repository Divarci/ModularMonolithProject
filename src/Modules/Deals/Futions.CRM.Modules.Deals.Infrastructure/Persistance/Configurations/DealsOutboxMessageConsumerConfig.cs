using Futions.CRM.Modules.Deals.Domain.OutboxMessages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Futions.CRM.Modules.Deals.Infrastructure.Persistance.Configurations;
public class DealsOutboxMessageConsumerConfig : IEntityTypeConfiguration<DealsOutboxMessageConsumer>
{
    public void Configure(EntityTypeBuilder<DealsOutboxMessageConsumer> builder)
    {
        builder.Property(o => o.Name)
            .IsRequired()
            .HasMaxLength(500);

        builder.HasKey(o => new { o.Id, o.Name });
    }
}
