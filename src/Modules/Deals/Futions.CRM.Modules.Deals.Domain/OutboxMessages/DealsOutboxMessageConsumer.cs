using Futions.CRM.Common.Domain.Entities.MessageConsumers;

namespace Futions.CRM.Modules.Deals.Domain.OutboxMessages;
public class DealsOutboxMessageConsumer : MessageConsumer, IMessageConsumerFactory<DealsOutboxMessageConsumer>
{
    public DealsOutboxMessageConsumer() { }

    private DealsOutboxMessageConsumer(Guid id, string name)
        : base(id, name) { }

    public DealsOutboxMessageConsumer Create(Guid id, string name)
        => new(id, name);
}
