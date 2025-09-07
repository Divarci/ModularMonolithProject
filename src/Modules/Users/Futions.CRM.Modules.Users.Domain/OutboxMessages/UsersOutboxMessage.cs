using Futions.CRM.Common.Domain.Entities.Messages;

namespace Futions.CRM.Modules.Users.Domain.OutboxMessages;
public class UsersOutboxMessage : Message, IMessageFactory<UsersOutboxMessage>
{
    public UsersOutboxMessage() { }

    private UsersOutboxMessage(
        Guid id,
        string type,
        string content,
        DateTime occurredOnUtc) : base(id, type, content, occurredOnUtc) { }

    public UsersOutboxMessage Create(Guid id, string type,
        string content, DateTime occurredOnUtc)
        => new(id, type, content, occurredOnUtc);
}
