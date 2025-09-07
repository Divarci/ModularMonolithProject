using Futions.CRM.Common.Domain.Entities.MessageConsumers;

namespace Futions.CRM.Modules.Users.Domain.OutboxMessages;
public class UsersOutboxMessageConsumer : MessageConsumer, IMessageConsumerFactory<UsersOutboxMessageConsumer>
{
    public UsersOutboxMessageConsumer() { }

    private UsersOutboxMessageConsumer(Guid id, string name)
        : base(id, name) { }

    public UsersOutboxMessageConsumer Create(Guid id, string name)
        => new(id, name);
}
