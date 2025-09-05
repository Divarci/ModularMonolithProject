using Futions.CRM.Common.Domain.Abstractions.IGenericRepositoies;
using Futions.CRM.Common.Domain.Entities;
using Futions.CRM.Common.Infrastructure.GenericRepositories;
using Futions.CRM.Modules.Users.Domain.Abstractions;
using Futions.CRM.Modules.Users.Infrastructure.Persistance.Database;

namespace Futions.CRM.Modules.Users.Infrastructure.UnitOfWorks;
public class UsersUnitOfWork(UsersDbContext context) : IUsersUnitOfWork
{
    private readonly UsersDbContext _context = context;

    public async Task CommitAsync(CancellationToken cancellationToken = default)
        => await _context.SaveChangesAsync(cancellationToken);

    public IReadRepository<TEntity> GetReadRepository<TEntity>()
        where TEntity : class, IAggregate
        => new ReadRepository<TEntity>(_context);

    public IWriteRepository<TEntity> GetWriteRepository<TEntity>()
        where TEntity : class, IRootAggregate
        => new WriteRepository<TEntity>(_context);
}
