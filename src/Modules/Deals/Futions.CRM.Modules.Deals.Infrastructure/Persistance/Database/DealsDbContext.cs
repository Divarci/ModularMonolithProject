using Microsoft.EntityFrameworkCore;

namespace Futions.CRM.Modules.Deals.Infrastructure.Persistance.Database;
public sealed class DealsDbContext(DbContextOptions<DealsDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Deals);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DealsDbContext).Assembly);

    }
}
