using Futions.CRM.Common.Domain.Entities;
using Futions.CRM.Common.Domain.IGenericRepositories;
using Microsoft.EntityFrameworkCore;

namespace Futions.CRM.Common.Infrastructure.GenericRepositories;
public sealed class ReadRepository<TEntity, TContext>(TContext context) : IReadRepository<TEntity>
    where TEntity : class, IAggregate
    where TContext : DbContext
{
    private readonly TContext _context = context;

    public IQueryable<TEntity> GetAll()
        => _context
            .Set<TEntity>()
            .AsNoTracking()
            .AsQueryable();

    public TResult Query<TResult>(Func<IQueryable<TEntity>, TResult> query)
        => query(GetAll());

    public Task<TResult> QueryAsync<TResult>(Func<IQueryable<TEntity>, Task<TResult>> query)
        => query(GetAll());

    public async Task<TEntity?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
        => await _context
            .Set<TEntity>()
            .FindAsync([id, cancellationToken], cancellationToken);
}
