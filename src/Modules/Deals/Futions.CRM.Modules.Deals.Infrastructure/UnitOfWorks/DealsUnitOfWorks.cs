using Futions.CRM.Common.Domain.Entities;
using Futions.CRM.Common.Domain.IGenericRepositories;
using Futions.CRM.Common.Infrastructure.GenericRepositories;
using Futions.CRM.Modules.Deals.Domain.Abstractions;
using Futions.CRM.Modules.Deals.Infrastructure.Persistance.Database;

namespace Futions.CRM.Modules.Deals.Infrastructure.UnitOfWorks;
public class DealsUnitOfWork(DealsDbContext context) : IDealsUnitOfWork
{
    private readonly DealsDbContext _context = context;

    public async Task CommitAsync(CancellationToken cancellationToken = default)
        => await _context.SaveChangesAsync(cancellationToken);

    public IReadRepository<TEntity> GetReadRepository<TEntity>()
        where TEntity : class, IAggregate
        => new ReadRepository<TEntity>(_context);

    public IWriteRepository<TEntity> GetWriteRepository<TEntity>()
        where TEntity : class, IRootAggregate
        => new WriteRepository<TEntity>(_context);
}
