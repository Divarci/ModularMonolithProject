using Futions.CRM.Modules.Deals.Domain.Deals;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Futions.CRM.Modules.Deals.Infrastructure.Persistance.Configurations;
public class DealConfig : IEntityTypeConfiguration<Deal>
{
    public void Configure(EntityTypeBuilder<Deal> builder)
    {
        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(64);

        builder.HasMany(x => x.DealProducts)
            .WithOne(x => x.Deal)
            .HasForeignKey(x => x.DealId)
            .OnDelete(DeleteBehavior.ClientCascade);
    }
}
