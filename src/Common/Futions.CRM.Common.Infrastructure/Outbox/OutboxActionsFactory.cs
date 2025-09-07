using Futions.CRM.Common.Domain.Abstractions.IGenericRepositoies;
using Futions.CRM.Common.Domain.Abstractions.IUnitOfWorks;
using Futions.CRM.Common.Domain.Entities.Messages;
using Futions.CRM.Common.Domain.Exceptions;
using Futions.CRM.Common.Domain.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Futions.CRM.Common.Infrastructure.Outbox;
public static class OutboxActionsFactory<TMessage> where TMessage : Message
{
    public static Func<IReadOnlyCollection<TMessage>, CancellationToken, Task> Create<TUnitOfWork>(
        IServiceProvider provider)
        where TUnitOfWork : IUnitOfWork
    {
        return async (messages, cancellationToken) =>
        {
            TUnitOfWork unitOfWork = provider.GetRequiredService<TUnitOfWork>();

            if (unitOfWork is null)
            {
                throw new CrmException(nameof(OutboxActionsFactory<TMessage>),
                    Error.Problem("OutboxCreate.NullError", "Unit Of Work not found"));
            }
            
            IWriteRepository<TMessage> outboxRepository = unitOfWork.GetWriteRepository<TMessage>();

            if (outboxRepository is null)
            {
                throw new CrmException(nameof(OutboxActionsFactory<TMessage>),
                    Error.Problem("OutboxCreate.NullError", "Write repository not found"));
            }

            await outboxRepository.CreateRangeAsync(messages, cancellationToken);

            await unitOfWork.CommitAsync(cancellationToken);
        };
    }

    public static async Task<IReadOnlyList<OutboxMessageResponse>> GetMessages<TUnitOfWork>(
        IServiceScopeFactory provider,
        IOutboxOptions options,
        CancellationToken cancellationToken = default)
        where TUnitOfWork : IUnitOfWork
    {
        using IServiceScope scope = provider.CreateScope();

        TUnitOfWork unitOfWork = scope.ServiceProvider.GetRequiredService<TUnitOfWork>();

        if (unitOfWork is null)
        {
            throw new CrmException(nameof(OutboxActionsFactory<TMessage>),
                Error.Problem("OutboxGetAll.NullError", "Unit Of Work not found"));
        }

        IReadRepository<TMessage> outboxRepository = unitOfWork.GetReadRepository<TMessage>();

        if (outboxRepository is null)
        {
            throw new CrmException(nameof(OutboxActionsFactory<TMessage>),
                Error.Problem("OutboxGetAll.NullError", "Read repository not found"));
        }

        List<OutboxMessageResponse> messages = await outboxRepository.GetAll()
            .Where(x => !x.ProcessedOnUtc.HasValue)
            .OrderBy(x => x.OccurredOnUtc)
            .Take(options.BatchSize)
            .Select(om => new OutboxMessageResponse(om.Id, om.Content))
            .ToListAsync(cancellationToken);

        return messages.AsReadOnly();
    }

    public static async Task Update<TUnitOfWork>(
        IServiceScopeFactory provider,
        OutboxMessageResponse outboxMessage,
        Exception? exception,
        CancellationToken cancellationToken = default)
        where TUnitOfWork : IUnitOfWork
    {
        using IServiceScope scope = provider.CreateScope();

        TUnitOfWork unitOfWork = scope.ServiceProvider.GetRequiredService<TUnitOfWork>();

        if (unitOfWork is null)
        {
            throw new CrmException(nameof(OutboxActionsFactory<TMessage>),
                Error.Problem("OutboxUpdate.NullError", "Unit Of Work not found"));
        }

        IWriteRepository<TMessage> outboxWriteRepository = unitOfWork.GetWriteRepository<TMessage>();

        if (outboxWriteRepository is null)
        {
            throw new CrmException(nameof(OutboxActionsFactory<TMessage>),
                Error.Problem("OutboxUpdate.NullError", "Write repository not found"));
        }

        IReadRepository<TMessage> outboxReadRepository = unitOfWork.GetReadRepository<TMessage>();

        if (outboxReadRepository is null)
        {
            throw new CrmException(nameof(OutboxActionsFactory<TMessage>),
                Error.Problem("OutboxUpdate.NullError", "Read repository not found"));
        }


        TMessage message = await outboxReadRepository
            .GetByIdAsync(outboxMessage.Id, cancellationToken);

        if (message is null)
        {
            throw new CrmException(nameof(OutboxActionsFactory<TMessage>),
                Error.Problem("OutboxUpdate.NullError", "Message not found"));
        }

        message.Update(exception?.ToString());

        outboxWriteRepository.Update(message);

        await unitOfWork.CommitAsync(cancellationToken);
    }
}
