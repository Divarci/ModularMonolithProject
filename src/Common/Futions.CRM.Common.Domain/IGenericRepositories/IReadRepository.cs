using Futions.CRM.Common.Domain.Entities;

namespace Futions.CRM.Common.Domain.IGenericRepositories;
public interface IReadRepository<TEntity> where TEntity : class, IAggregate
{
    IQueryable<TEntity> GetAll();

    TResult Query<TResult>(
        Func<IQueryable<TEntity>, TResult> query);

    Task<TResult> QueryAsync<TResult>(
        Func<IQueryable<TEntity>, Task<TResult>> query);

    Task<TEntity?> GetByIdAsync(
      Guid id,
      CancellationToken cancellationToken = default);
}
