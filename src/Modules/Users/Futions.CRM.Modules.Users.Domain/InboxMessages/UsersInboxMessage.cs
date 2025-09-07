using Futions.CRM.Common.Domain.Entities.Messages;

namespace Futions.CRM.Modules.Users.Domain.InboxMessages;
public class UsersInboxMessage : Message, IMessageFactory<UsersInboxMessage>
{
    public UsersInboxMessage() { }

    public UsersInboxMessage(
        Guid id,
        string type,
        string content,
        DateTime occurredOnUtc) : base(id, type, content, occurredOnUtc) { }

    public UsersInboxMessage Create(Guid id, string type,
        string content, DateTime occurredOnUtc)
        => new(id, type, content, occurredOnUtc);
}
