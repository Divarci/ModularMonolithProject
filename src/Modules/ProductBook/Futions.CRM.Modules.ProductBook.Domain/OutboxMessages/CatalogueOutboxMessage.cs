using Futions.CRM.Common.Domain.Entities.Messages;

namespace Futions.CRM.Modules.Catalogue.Domain.OutboxMessages;
public sealed class CatalogueOutboxMessage : Message, IMessageFactory<CatalogueOutboxMessage>
{
    public CatalogueOutboxMessage() { }

    private CatalogueOutboxMessage(
        Guid id, 
        string type,
        string content, 
        DateTime occurredOnUtc) : base(id, type, content, occurredOnUtc) { }

    public CatalogueOutboxMessage Create(Guid id, string type,
        string content, DateTime occurredOnUtc)
        => new(id, type, content, occurredOnUtc);
}
