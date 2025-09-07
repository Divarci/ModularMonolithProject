using Futions.CRM.Common.Domain.Entities.Messages;

namespace Futions.CRM.Modules.Catalogue.Domain.InboxMessages;
public sealed class CatalogueInboxMessage : Message, IMessageFactory<CatalogueInboxMessage>
{
    public CatalogueInboxMessage() { }

    public CatalogueInboxMessage(
        Guid id, 
        string type, 
        string content, 
        DateTime occurredOnUtc) : base(id, type, content, occurredOnUtc) { }

    public CatalogueInboxMessage Create(Guid id, string type,
        string content, DateTime occurredOnUtc)
        => new(id, type, content, occurredOnUtc);
}
