using Futions.CRM.Common.Domain.Entities.MessageConsumers;

namespace Futions.CRM.Modules.Deals.Domain.InboxMessages;
public class DealsInboxMessageConsumer : MessageConsumer, IMessageConsumerFactory<DealsInboxMessageConsumer>
{
    public DealsInboxMessageConsumer() { }

    public DealsInboxMessageConsumer(Guid id, string name)
        : base(id, name) { }

    public DealsInboxMessageConsumer Create(Guid id, string name)
        => new(id, name);
}

