using Futions.CRM.Common.Domain.Entities;
using Futions.CRM.Common.Domain.IGenericRepositories;

namespace Futions.CRM.Common.Domain.IUnitOfWorks;
public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken cancellationToken = default);

    IReadRepository<TEntity> GetReadRepository<TEntity>()
        where TEntity : class, IAggregate;

    IWriteRepository<TEntity> GetWriteRepository<TEntity>()
        where TEntity : class, IRootAggregate;
}
