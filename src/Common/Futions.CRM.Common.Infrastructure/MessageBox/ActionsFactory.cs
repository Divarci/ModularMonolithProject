using Futions.CRM.Common.Domain.Abstractions.IGenericRepositoies;
using Futions.CRM.Common.Domain.Abstractions.IUnitOfWorks;
using Futions.CRM.Common.Domain.Entities.Messages;
using Futions.CRM.Common.Domain.Exceptions;
using Futions.CRM.Common.Domain.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Futions.CRM.Common.Infrastructure.MessageBox;
public sealed class ActionsFactory<TMessage> where TMessage : Message
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
                throw new CrmException(nameof(ActionsFactory<TMessage>),
                    Error.Problem("Create.NullError", "Unit Of Work not found"));
            }

            IWriteRepository<TMessage> messageBoxRepository = unitOfWork.GetWriteRepository<TMessage>();

            if (messageBoxRepository is null)
            {
                throw new CrmException(nameof(ActionsFactory<TMessage>),
                    Error.Problem("Create.NullError", "Write repository not found"));
            }

            await messageBoxRepository.CreateRangeAsync(messages, cancellationToken);

            await unitOfWork.CommitAsync(cancellationToken);
        };
    }

    public static async Task<IReadOnlyList<MessageResponse>> GetMessages<TUnitOfWork>(
        IServiceScopeFactory provider,
        IMessageBoxOptions options,
        CancellationToken cancellationToken = default)
        where TUnitOfWork : IUnitOfWork
    {
        using IServiceScope scope = provider.CreateScope();

        TUnitOfWork unitOfWork = scope.ServiceProvider.GetRequiredService<TUnitOfWork>();

        if (unitOfWork is null)
        {
            throw new CrmException(nameof(ActionsFactory<TMessage>),
                Error.Problem("GetAll.NullError", "Unit Of Work not found"));
        }

        IReadRepository<TMessage> messageRepository = unitOfWork.GetReadRepository<TMessage>();

        if (messageRepository is null)
        {
            throw new CrmException(nameof(ActionsFactory<TMessage>),
                Error.Problem("GetAll.NullError", "Read repository not found"));
        }

        List<MessageResponse> messages = await messageRepository.GetAll()
            .Where(x => !x.ProcessedOnUtc.HasValue)
            .OrderBy(x => x.OccurredOnUtc)
            .Take(options.BatchSize)
            .Select(om => new MessageResponse(om.Id, om.Content))
            .ToListAsync(cancellationToken);

        return messages.AsReadOnly();
    }

    public static async Task Update<TUnitOfWork>(
        IServiceScopeFactory provider,
        MessageResponse inboxMessage,
        Exception? exception,
        CancellationToken cancellationToken = default)
        where TUnitOfWork : IUnitOfWork
    {
        using IServiceScope scope = provider.CreateScope();

        TUnitOfWork unitOfWork = scope.ServiceProvider.GetRequiredService<TUnitOfWork>();

        if (unitOfWork is null)
        {
            throw new CrmException(nameof(ActionsFactory<TMessage>),
                Error.Problem("Update.NullError", "Unit Of Work not found"));
        }

        IWriteRepository<TMessage> writeRepository = unitOfWork.GetWriteRepository<TMessage>();

        if (writeRepository is null)
        {
            throw new CrmException(nameof(ActionsFactory<TMessage>),
                Error.Problem("InboxUpdate.NullError", "Write repository not found"));
        }

        IReadRepository<TMessage> readRepository = unitOfWork.GetReadRepository<TMessage>();

        if (readRepository is null)
        {
            throw new CrmException(nameof(ActionsFactory<TMessage>),
                Error.Problem("InboxUpdate.NullError", "Read repository not found"));
        }


        TMessage message = await readRepository
            .GetByIdAsync(inboxMessage.Id, cancellationToken);

        if (message is null)
        {
            throw new CrmException(nameof(ActionsFactory<TMessage>),
                Error.Problem("InboxUpdate.NullError", "Message not found"));
        }

        message.Update(exception?.ToString());

        writeRepository.Update(message);

        await unitOfWork.CommitAsync(cancellationToken);
    }
}
