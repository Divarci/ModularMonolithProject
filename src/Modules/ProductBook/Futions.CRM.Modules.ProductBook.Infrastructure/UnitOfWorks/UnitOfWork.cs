using Futions.CRM.Common.Domain.Entities;
using Futions.CRM.Common.Domain.IGenericRepositories;
using Futions.CRM.Common.Domain.IUnitOfWorks;
using Futions.CRM.Common.Infrastructure.GenericRepositories;
using Microsoft.EntityFrameworkCore;

namespace Futions.CRM.Modules.Catalogue.Infrastructure.UnitOfWorks;
public class UnitOfWork(DbContext context) : IUnitOfWork
{
    private readonly DbContext _context = context;

    public async Task CommitAsync(CancellationToken cancellationToken = default)
        => await _context.SaveChangesAsync(cancellationToken);

    public IReadRepository<TEntity> GetReadRepository<TEntity>()
        where TEntity : class, IAggregate
        => new ReadRepository<TEntity, DbContext>(_context);

    public IWriteRepository<TEntity> GetWriteRepository<TEntity>()
        where TEntity : class, IRootAggregate
        => new WriteRepository<TEntity, DbContext>(_context);
}
