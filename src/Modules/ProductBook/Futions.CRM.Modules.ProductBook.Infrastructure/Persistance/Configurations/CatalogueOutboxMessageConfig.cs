using Futions.CRM.Modules.Catalogue.Domain.OutboxMessages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Futions.CRM.Modules.Catalogue.Infrastructure.Persistance.Configurations;
public class CatalogueOutboxMessageConfig : IEntityTypeConfiguration<CatalogueOutboxMessage>
{
    public void Configure(EntityTypeBuilder<CatalogueOutboxMessage> builder)
    {
        builder.Property(o => o.Content)
            .HasColumnType("nvarchar(max)");
    }
}
