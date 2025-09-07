using Futions.CRM.Modules.Catalogue.Domain.InboxMessages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Futions.CRM.Modules.Catalogue.Infrastructure.Persistance.Configurations;
public class CatalogueInboxMessageConfig : IEntityTypeConfiguration<CatalogueInboxMessage>
{
    public void Configure(EntityTypeBuilder<CatalogueInboxMessage> builder)
    {
        builder.Property(o => o.Content)
            .HasColumnType("nvarchar(max)");
    }
}
