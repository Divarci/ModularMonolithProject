using Microsoft.EntityFrameworkCore;

namespace Futions.CRM.Modules.Catalogue.Infrastructure.Persistance.Database;
public sealed class CatalogueDbContext(DbContextOptions<CatalogueDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Catalogue);
    }
}
