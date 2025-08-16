using Futions.CRM.Modules.Catalogue.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Futions.CRM.Modules.Catalogue.Infrastructure.Persistance.Configurations;
public class ProductConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(512);

        builder.Property(x => x.Price)
            .IsRequired()
            .HasPrecision(18, 2);
    }
}
