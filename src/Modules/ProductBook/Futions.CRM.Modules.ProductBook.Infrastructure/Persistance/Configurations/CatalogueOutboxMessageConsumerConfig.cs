using Futions.CRM.Modules.Catalogue.Domain.OutboxMessages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Futions.CRM.Modules.Catalogue.Infrastructure.Persistance.Configurations;
public class CatalogueOutboxMessageConsumerConfig : IEntityTypeConfiguration<CatalogueOutboxMessageConsumer>
{
    public void Configure(EntityTypeBuilder<CatalogueOutboxMessageConsumer> builder)
    {
        builder.Property(o => o.Name)
            .IsRequired()
            .HasMaxLength(500);

        builder.HasKey(o => new { o.Id, o.Name });
    }
}
