using Futions.CRM.Common.Domain.Entities.MessageConsumers;

namespace Futions.CRM.Modules.Users.Domain.InboxMessages;
public class UsersInboxMessageConsumer : MessageConsumer, IMessageConsumerFactory<UsersInboxMessageConsumer>
{
    public UsersInboxMessageConsumer() { }

    public UsersInboxMessageConsumer(Guid id, string name)
        : base(id, name) { }

    public UsersInboxMessageConsumer Create(Guid id, string name)
        => new(id, name);
}
