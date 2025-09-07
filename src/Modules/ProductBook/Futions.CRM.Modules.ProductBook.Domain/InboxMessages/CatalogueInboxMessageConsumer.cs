using Futions.CRM.Common.Domain.Entities.MessageConsumers;

namespace Futions.CRM.Modules.Catalogue.Domain.InboxMessages;
public class CatalogueInboxMessageConsumer : MessageConsumer, IMessageConsumerFactory<CatalogueInboxMessageConsumer>
{
    public CatalogueInboxMessageConsumer() { }

    public CatalogueInboxMessageConsumer(Guid id, string name)
        : base(id, name) { }

    public CatalogueInboxMessageConsumer Create(Guid id, string name)
        => new(id, name);
}

