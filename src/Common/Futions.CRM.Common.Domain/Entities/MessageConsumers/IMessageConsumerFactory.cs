namespace Futions.CRM.Common.Domain.Entities.MessageConsumers;
public interface IMessageConsumerFactory<out TMessageConsumer> 
    where TMessageConsumer : MessageConsumer
{
    TMessageConsumer Create(Guid id, string name);
}
