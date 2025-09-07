using Futions.CRM.Common.Domain.Entities.MessageConsumers;

namespace Futions.CRM.Modules.Catalogue.Domain.OutboxMessages;
public class CatalogueOutboxMessageConsumer : MessageConsumer, IMessageConsumerFactory<CatalogueOutboxMessageConsumer>
{
    public CatalogueOutboxMessageConsumer() { }

    private CatalogueOutboxMessageConsumer(Guid id, string name)
        : base(id, name) { }

    public CatalogueOutboxMessageConsumer Create(Guid id, string name)
        => new(id, name);
}
