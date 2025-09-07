using Futions.CRM.Modules.Catalogue.Domain.InboxMessages;
using Futions.CRM.Modules.Catalogue.Domain.OutboxMessages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Futions.CRM.Modules.Catalogue.Infrastructure.Persistance.Configurations;
public class CatalogueInboxMessageConsumerConfig : IEntityTypeConfiguration<CatalogueInboxMessageConsumer>
{
    public void Configure(EntityTypeBuilder<CatalogueInboxMessageConsumer> builder)
    {
        builder.Property(o => o.Name)
            .IsRequired()
            .HasMaxLength(500);

        builder.HasKey(o => new { o.Id, o.Name });
    }
}
