using Futions.CRM.Common.Domain.Entities.OutboxMessages;

namespace Futions.CRM.Modules.Users.Domain.OutboxMessages;
public class UsersOutboxMessage : OutboxMessage, IOutboxMessageFactory<UsersOutboxMessage>
{
    public UsersOutboxMessage() { }

    public UsersOutboxMessage(
        Guid id,
        string type,
        string content,
        DateTime occurredOnUtc) : base(id, type, content, occurredOnUtc) { }

    public UsersOutboxMessage Create(Guid id, string type,
        string content, DateTime occurredOnUtc)
        => new(id, type, content, occurredOnUtc);
}

