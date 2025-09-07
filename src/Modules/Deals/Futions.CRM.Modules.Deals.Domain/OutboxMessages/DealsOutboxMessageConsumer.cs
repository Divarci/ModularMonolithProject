using Futions.CRM.Common.Domain.Entities.OutboxMessageConsumers;

namespace Futions.CRM.Modules.Deals.Domain.OutboxMessages;
public class DealsOutboxMessageConsumer : OutboxMessageConsumer, IOutboxMessageConsumerFactory<DealsOutboxMessageConsumer>
{
    public DealsOutboxMessageConsumer() { }

    private DealsOutboxMessageConsumer(Guid outboxMessageId, string name)
        : base(outboxMessageId, name) { }

    public DealsOutboxMessageConsumer Create(Guid outboxMessageId, string name)
        => new(outboxMessageId, name);
}
