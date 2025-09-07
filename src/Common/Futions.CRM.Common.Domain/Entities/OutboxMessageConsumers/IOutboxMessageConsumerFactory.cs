namespace Futions.CRM.Common.Domain.Entities.OutboxMessageConsumers;
public interface IOutboxMessageConsumerFactory<out TMessageConsumer> 
    where TMessageConsumer : OutboxMessageConsumer
{
    TMessageConsumer Create(Guid outboxMessageId, string name);
}
