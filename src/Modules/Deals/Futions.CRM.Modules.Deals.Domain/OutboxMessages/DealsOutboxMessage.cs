using Futions.CRM.Common.Domain.Entities.OutboxMessages;

namespace Futions.CRM.Modules.Deals.Domain.OutboxMessages;
public sealed class DealsOutboxMessage : OutboxMessage, IOutboxMessageFactory<DealsOutboxMessage>
{
    public DealsOutboxMessage() { }

    public DealsOutboxMessage(
        Guid id,
        string type,
        string content,
        DateTime occurredOnUtc) : base(id, type, content, occurredOnUtc) { }

    public DealsOutboxMessage Create(Guid id, string type,
        string content, DateTime occurredOnUtc)
        => new(id, type, content, occurredOnUtc);
}
