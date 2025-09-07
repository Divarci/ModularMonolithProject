using Futions.CRM.Common.Domain.Entities.Messages;

namespace Futions.CRM.Modules.Deals.Domain.InboxMessages;
public class DealsInboxMessage : Message, IMessageFactory<DealsInboxMessage>
{
    public DealsInboxMessage() { }

    public DealsInboxMessage(
        Guid id,
        string type,
        string content,
        DateTime occurredOnUtc) : base(id, type, content, occurredOnUtc) { }

    public DealsInboxMessage Create(Guid id, string type,
        string content, DateTime occurredOnUtc)
        => new(id, type, content, occurredOnUtc);
}
