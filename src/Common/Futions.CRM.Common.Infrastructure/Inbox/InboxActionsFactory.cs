using Futions.CRM.Common.Domain.Abstractions.IGenericRepositoies;
using Futions.CRM.Common.Domain.Abstractions.IUnitOfWorks;
using Futions.CRM.Common.Domain.Entities.Messages;
using Futions.CRM.Common.Domain.Exceptions;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Common.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Futions.CRM.Common.Infrastructure.Inbox;
public static class InboxActionsFactory<TMessage> where TMessage : Message
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
                throw new CrmException(nameof(InboxActionsFactory<TMessage>),
                    Error.Problem("InboxCreate.NullError", "Unit Of Work not found"));
            }
            
            IWriteRepository<TMessage> inboxRepository = unitOfWork.GetWriteRepository<TMessage>();

            if (inboxRepository is null)
            {
                throw new CrmException(nameof(InboxActionsFactory<TMessage>),
                    Error.Problem("InboxCreate.NullError", "Write repository not found"));
            }

            await inboxRepository.CreateRangeAsync(messages, cancellationToken);

            await unitOfWork.CommitAsync(cancellationToken);
        };
    }

    public static async Task<IReadOnlyList<InboxMessageResponse>> GetMessages<TUnitOfWork>(
        IServiceScopeFactory provider,
        IInboxOptions options,
        CancellationToken cancellationToken = default)
        where TUnitOfWork : IUnitOfWork
    {
        using IServiceScope scope = provider.CreateScope();

        TUnitOfWork unitOfWork = scope.ServiceProvider.GetRequiredService<TUnitOfWork>();

        if (unitOfWork is null)
        {
            throw new CrmException(nameof(InboxActionsFactory<TMessage>),
                Error.Problem("InboxGetAll.NullError", "Unit Of Work not found"));
        }

        IReadRepository<TMessage> inboxRepository = unitOfWork.GetReadRepository<TMessage>();

        if (inboxRepository is null)
        {
            throw new CrmException(nameof(InboxActionsFactory<TMessage>),
                Error.Problem("InboxGetAll.NullError", "Read repository not found"));
        }

        List<InboxMessageResponse> messages = await inboxRepository.GetAll()
            .Where(x => !x.ProcessedOnUtc.HasValue)
            .OrderBy(x => x.OccurredOnUtc)
            .Take(options.BatchSize)
            .Select(om => new InboxMessageResponse(om.Id, om.Content))
            .ToListAsync(cancellationToken);

        return messages.AsReadOnly();
    }

    public static async Task Update<TUnitOfWork>(
        IServiceScopeFactory provider,
        InboxMessageResponse inboxMessage,
        Exception? exception,
        CancellationToken cancellationToken = default)
        where TUnitOfWork : IUnitOfWork
    {
        using IServiceScope scope = provider.CreateScope();

        TUnitOfWork unitOfWork = scope.ServiceProvider.GetRequiredService<TUnitOfWork>();

        if (unitOfWork is null)
        {
            throw new CrmException(nameof(InboxActionsFactory<TMessage>),
                Error.Problem("InboxUpdate.NullError", "Unit Of Work not found"));
        }

        IWriteRepository<TMessage> inboxWriteRepository = unitOfWork.GetWriteRepository<TMessage>();

        if (inboxWriteRepository is null)
        {
            throw new CrmException(nameof(InboxActionsFactory<TMessage>),
                Error.Problem("InboxUpdate.NullError", "Write repository not found"));
        }

        IReadRepository<TMessage> inboxReadRepository = unitOfWork.GetReadRepository<TMessage>();

        if (inboxReadRepository is null)
        {
            throw new CrmException(nameof(InboxActionsFactory<TMessage>),
                Error.Problem("InboxUpdate.NullError", "Read repository not found"));
        }


        TMessage message = await inboxReadRepository
            .GetByIdAsync(inboxMessage.Id, cancellationToken);

        if (message is null)
        {
            throw new CrmException(nameof(InboxActionsFactory<TMessage>),
                Error.Problem("InboxUpdate.NullError", "Message not found"));
        }

        message.Update(exception?.ToString());

        inboxWriteRepository.Update(message);

        await unitOfWork.CommitAsync(cancellationToken);
    }
}
