using Futions.CRM.Common.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Futions.CRM.Modules.Users.Infrastructure.Persistance.Database;
public sealed class UsersDbContext(DbContextOptions<UsersDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Users);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UsersDbContext).Assembly);

        modelBuilder.AutoSeedData();
    }
}
