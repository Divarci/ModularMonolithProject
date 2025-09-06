using Futions.CRM.Common.Domain.Abstractions.Entities;
using Futions.CRM.Common.Domain.Abstractions.IGenericRepositoies;
using Microsoft.EntityFrameworkCore;

namespace Futions.CRM.Common.Infrastructure.GenericRepositories;
public sealed class ReadRepository<TEntity>(DbContext context) : IReadRepository<TEntity>
    where TEntity : class, IAggregate
{
    private readonly DbContext _context = context;

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
