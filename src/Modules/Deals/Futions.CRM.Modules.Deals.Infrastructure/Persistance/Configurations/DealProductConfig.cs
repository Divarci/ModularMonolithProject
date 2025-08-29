using Futions.CRM.Modules.Deals.Domain.Deals;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Futions.CRM.Modules.Deals.Infrastructure.Persistance.Configurations;
public class DealProductConfig : IEntityTypeConfiguration<DealProduct>
{
    public void Configure(EntityTypeBuilder<DealProduct> builder)
    {
        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(512);

        builder.Property(x => x.Price)
            .IsRequired()
            .HasPrecision(18,2);

        builder.Property(x => x.Discount)
            .HasPrecision(18, 2);

        builder.HasOne(x => x.Product)
            .WithMany(x => x.DealProducts)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.ClientCascade);
    }
}
