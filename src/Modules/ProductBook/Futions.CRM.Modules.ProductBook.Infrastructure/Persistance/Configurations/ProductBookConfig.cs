using Futions.CRM.Modules.Catalogue.Domain.ProductBooks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Futions.CRM.Modules.Catalogue.Infrastructure.Persistance.Configurations;
public class ProductBookConfig : IEntityTypeConfiguration<ProductBook>
{
    public void Configure(EntityTypeBuilder<ProductBook> builder)
    {
        builder.Property(x=>x.Title)
            .IsRequired()
            .HasMaxLength(64);

        builder.HasMany(x => x.Products)
            .WithOne(x=>x.ProductBook)
            .HasForeignKey(x=>x.ProductBookId)
            .OnDelete(DeleteBehavior.ClientCascade);
    }
}
