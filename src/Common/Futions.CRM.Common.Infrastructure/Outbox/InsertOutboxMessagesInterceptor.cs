using Futions.CRM.Common.Domain.Abstractions.Entities;
using Futions.CRM.Common.Domain.DomainEvents;
using Futions.CRM.Common.Domain.Entities.OutboxMessages;
using Futions.CRM.Common.Infrastructure.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;

namespace Futions.CRM.Common.Infrastructure.Outbox;
public sealed class InsertOutboxMessagesInterceptor<TMessage>(
    Func<IReadOnlyCollection<TMessage>, CancellationToken, Task> handler,
    IOutboxMessageFactory<TMessage> messageFactory) : SaveChangesInterceptor
    where TMessage : OutboxMessage, new()
{
    private readonly Func<IReadOnlyCollection<TMessage>, CancellationToken, Task> _saveOutboxMessages = handler;
    private readonly IOutboxMessageFactory<TMessage> _messageFactory = messageFactory;

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null)
        {
            IReadOnlyCollection<TMessage> messages = ExtractOutboxMessages(eventData.Context, _messageFactory);

            if (messages.Count > 0)
            {
                await _saveOutboxMessages(messages, cancellationToken);
            }
        }

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static IReadOnlyCollection<TMessage> ExtractOutboxMessages(
        DbContext context, IOutboxMessageFactory<TMessage> message)
    {
        IReadOnlyCollection<TMessage> outboxMessages = context
            .ChangeTracker
            .Entries<BaseEntity>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                IReadOnlyCollection<IDomainEvent> domainEvents = entity.DomainEvents;

                entity.ClearDomainEvents();

                return domainEvents;
            })
            .Select(de => message.Create(
                de.Id,
                de.GetType().Name,
                JsonConvert.SerializeObject(de,SerializerSettings.Instance),
                de.OccurredOnUtc))
            .ToList().AsReadOnly();

        return outboxMessages;
    }
}
