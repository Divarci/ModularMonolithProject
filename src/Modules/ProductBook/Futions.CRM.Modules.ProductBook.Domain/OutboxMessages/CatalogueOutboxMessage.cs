using Futions.CRM.Common.Domain.Entities.OutboxMessages;

namespace Futions.CRM.Modules.Catalogue.Domain.OutboxMessages;
public sealed class CatalogueOutboxMessage : OutboxMessage, IOutboxMessageFactory<CatalogueOutboxMessage>
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
