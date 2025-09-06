using Futions.CRM.Common.Domain.Abstractions.Entities;
using Futions.CRM.Common.Domain.Abstractions.Entities.Extensions;
using Futions.CRM.Common.Domain.Abstractions.IGenericRepositoies;

namespace Futions.CRM.Common.Domain.Abstractions.IUnitOfWorks;
public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken cancellationToken = default);

    IReadRepository<TEntity> GetReadRepository<TEntity>()
        where TEntity : class, IAggregate;

    IWriteRepository<TEntity> GetWriteRepository<TEntity>()
        where TEntity : class, IRootAggregate;    
}
