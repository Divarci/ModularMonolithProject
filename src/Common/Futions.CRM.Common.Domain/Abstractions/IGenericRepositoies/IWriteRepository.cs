using Futions.CRM.Common.Domain.Entities;

namespace Futions.CRM.Common.Domain.Abstractions.IGenericRepositoies;
public interface IWriteRepository<TEntity> where TEntity : class, IRootAggregate
{
    Task<TEntity> CreateAsync(
        TEntity entity,
        CancellationToken cancellationToken = default);

    Task CreateRangeAsync(
        IEnumerable<TEntity> entityCollection,
        CancellationToken cancellationToken = default);

    TEntity Update(TEntity entity);

    void UpdateRange(IEnumerable<TEntity> entityCollection);

    void Delete(TEntity entity);

    void DeleteRange(IEnumerable<TEntity> entityCollection);
}
