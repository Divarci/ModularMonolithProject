using Futions.CRM.Common.Domain.Entities.OutboxMessageConsumers;

namespace Futions.CRM.Modules.Users.Domain.OutboxMessages;
public class UsersOutboxMessageConsumer : OutboxMessageConsumer, IOutboxMessageConsumerFactory<UsersOutboxMessageConsumer>
{
    public UsersOutboxMessageConsumer() { }

    private UsersOutboxMessageConsumer(Guid outboxMessageId, string name)
        : base(outboxMessageId, name) { }

    public UsersOutboxMessageConsumer Create(Guid outboxMessageId, string name)
        => new(outboxMessageId, name);
}
