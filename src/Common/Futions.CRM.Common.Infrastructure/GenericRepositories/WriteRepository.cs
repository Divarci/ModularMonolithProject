using Futions.CRM.Common.Domain.Entities;
using Futions.CRM.Common.Domain.IGenericRepositories;
using Microsoft.EntityFrameworkCore;

namespace Futions.CRM.Common.Infrastructure.GenericRepositories;
public sealed class WriteRepository<TEntity>(DbContext context) : IWriteRepository<TEntity>
    where TEntity : class, IRootAggregate
{
    private readonly DbContext _context = context;

    public async Task<TEntity> CreateAsync(
       TEntity entity,
       CancellationToken cancellationToken = default)
    {
        await _context
            .Set<TEntity>()
            .AddAsync(entity, cancellationToken);

        return entity;
    }

    public async Task<TEntity> CreateAsync<TData>(
      TEntity entity, IEnumerable<TData> attachedItems,
      CancellationToken cancellationToken = default) where TData : class
    {
        foreach (TData item in attachedItems)
        {
            _context.Attach(item);
        }

        await _context
            .Set<TEntity>()
            .AddAsync(entity, cancellationToken);

        return entity;
    }

    public async Task CreateRangeAsync(
        IEnumerable<TEntity> entityCollection,
        CancellationToken cancellationToken = default)
        => await _context
            .Set<TEntity>()
            .AddRangeAsync(entityCollection, cancellationToken);

    public TEntity Update(TEntity entity)
    {
        _context.Set<TEntity>()
            .Update(entity);

        return entity;
    }

    public void UpdateRange(IEnumerable<TEntity> entityCollection)
        => _context
            .Set<TEntity>()
            .UpdateRange(entityCollection);

    public void Delete(TEntity entity)
        => _context
            .Set<TEntity>()
            .Remove(entity);

    public void DeleteRange(IEnumerable<TEntity> entityCollection)
        => _context
            .Set<TEntity>()
            .RemoveRange(entityCollection);
}
