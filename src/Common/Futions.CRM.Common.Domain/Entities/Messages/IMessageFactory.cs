namespace Futions.CRM.Common.Domain.Entities.Messages;
public interface IMessageFactory<out TMessage> where TMessage : Message
{
    TMessage Create(Guid id, string type, string content, DateTime occurredOnUtc);
}
