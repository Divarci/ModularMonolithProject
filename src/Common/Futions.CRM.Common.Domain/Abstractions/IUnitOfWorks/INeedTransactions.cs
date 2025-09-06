namespace Futions.CRM.Common.Domain.Abstractions.IUnitOfWorks;
public interface INeedTransactions : IDisposable
{
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);

    Task CommitTransactionAsync(CancellationToken cancellationToken = default);

    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}
