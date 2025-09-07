using Futions.CRM.Modules.Deals.Domain.InboxMessages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Futions.CRM.Modules.Deals.Infrastructure.Persistance.Configurations;
public class DealsInboxMessageConsumerConfig : IEntityTypeConfiguration<DealsInboxMessageConsumer>
{
    public void Configure(EntityTypeBuilder<DealsInboxMessageConsumer> builder)
    {
        builder.Property(o => o.Name)
            .IsRequired()
            .HasMaxLength(500);

        builder.HasKey(o => new { o.Id, o.Name });
    }
}
