using Futions.CRM.Common.Domain.Entities;
using Futions.CRM.Common.Domain.IGenericRepositories;
using Futions.CRM.Common.Domain.IUnitOfWorks;
using Futions.CRM.Common.Infrastructure.GenericRepositories;
using Futions.CRM.Modules.Catalogue.Domain.Abstractions;
using Futions.CRM.Modules.Catalogue.Infrastructure.Persistance.Database;

namespace Futions.CRM.Modules.Catalogue.Infrastructure.UnitOfWorks;
public class CatalogueUnitOfWork(CatalogueDbContext context) : ICatalogueUnitOfWork
{
    private readonly CatalogueDbContext _context = context;

    public async Task CommitAsync(CancellationToken cancellationToken = default)
        => await _context.SaveChangesAsync(cancellationToken);

    public IReadRepository<TEntity> GetReadRepository<TEntity>()
        where TEntity : class, IAggregate
        => new ReadRepository<TEntity>(_context);

    public IWriteRepository<TEntity> GetWriteRepository<TEntity>()
        where TEntity : class, IRootAggregate
        => new WriteRepository<TEntity>(_context);
}
