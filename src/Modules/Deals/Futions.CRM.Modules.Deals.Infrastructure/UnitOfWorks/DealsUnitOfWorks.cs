using Futions.CRM.Common.Domain.Abstractions.Entities;
using Futions.CRM.Common.Domain.Abstractions.Entities.Extensions;
using Futions.CRM.Common.Domain.Abstractions.IGenericRepositoies;
using Futions.CRM.Common.Domain.Exceptions;
using Futions.CRM.Common.Infrastructure.GenericRepositories;
using Futions.CRM.Modules.Deals.Domain.Abstractions;
using Futions.CRM.Modules.Deals.Infrastructure.Persistance.Database;
using Microsoft.EntityFrameworkCore.Storage;

namespace Futions.CRM.Modules.Deals.Infrastructure.UnitOfWorks;
public class DealsUnitOfWork(DealsDbContext context) : IDealsUnitOfWork
{
    private readonly DealsDbContext _context = context;
    private IDbContextTransaction? _currentTransaction;

    public async Task CommitAsync(CancellationToken cancellationToken = default)
        => await _context.SaveChangesAsync(cancellationToken);

    public IReadRepository<TEntity> GetReadRepository<TEntity>()
        where TEntity : class, IAggregate
        => new ReadRepository<TEntity>(_context);

    public IWriteRepository<TEntity> GetWriteRepository<TEntity>()
        where TEntity : class, IRootAggregate
        => new WriteRepository<TEntity>(_context);

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction != null)
        {
            return;
        }

        _currentTransaction = await _context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction == null)
        {
            throw new CrmException("No active transaction.");
        }

        await _context.SaveChangesAsync(cancellationToken);

        await _currentTransaction.CommitAsync(cancellationToken);

        await _currentTransaction.DisposeAsync();

        _currentTransaction = null;
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction == null)
        {
            return;
        }

        await _currentTransaction.RollbackAsync(cancellationToken);

        await _currentTransaction.DisposeAsync();

        _currentTransaction = null;
    }

    public void Dispose()
    {
        _currentTransaction?.Dispose();
    }
}
