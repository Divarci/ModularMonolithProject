using Futions.CRM.Common.Domain.Entities.OutboxMessageConsumers;

namespace Futions.CRM.Modules.Catalogue.Domain.OutboxMessages;
public class CatalogueOutboxMessageConsumer : OutboxMessageConsumer, IOutboxMessageConsumerFactory<CatalogueOutboxMessageConsumer>
{
    public CatalogueOutboxMessageConsumer() { }

    private CatalogueOutboxMessageConsumer(Guid outboxMessageId, string name)
        : base(outboxMessageId, name) { }

    public CatalogueOutboxMessageConsumer Create(Guid outboxMessageId, string name)
        => new(outboxMessageId, name);
}
