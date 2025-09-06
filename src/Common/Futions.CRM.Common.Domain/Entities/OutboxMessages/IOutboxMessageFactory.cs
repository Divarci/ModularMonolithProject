namespace Futions.CRM.Common.Domain.Entities.OutboxMessages;
public interface IOutboxMessageFactory<out TMessage> where TMessage : OutboxMessage
{
    TMessage Create(Guid id, string type, string content, DateTime occurredOnUtc);
}
