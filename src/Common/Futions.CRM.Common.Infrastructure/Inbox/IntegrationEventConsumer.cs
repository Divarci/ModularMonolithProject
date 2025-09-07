using Futions.CRM.Common.Application.EventBus;
using Futions.CRM.Common.Domain.Abstractions.IUnitOfWorks;
using Futions.CRM.Common.Domain.Entities.Messages;
using Futions.CRM.Common.Infrastructure.Serialization;
using MassTransit;
using Newtonsoft.Json;


namespace Futions.CRM.Common.Infrastructure.Inbox;
public sealed class IntegrationEventConsumer<TIntegrationEvent, TUnitOfWork, TMessage>(
    TUnitOfWork unitOfWork,
    IMessageFactory<TMessage> messageFactory) : IConsumer<TIntegrationEvent>
    where TIntegrationEvent : IntegrationEvent
    where TUnitOfWork : IUnitOfWork
    where TMessage : Message
{
    private readonly TUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMessageFactory<TMessage> _messageFactory = messageFactory;

    public async Task Consume(ConsumeContext<TIntegrationEvent> context)
    {
        TIntegrationEvent integrationEvent = context.Message;

        TMessage inboxMessage = _messageFactory.Create(
            integrationEvent.Id,
            integrationEvent.GetType().Name,
            JsonConvert.SerializeObject(integrationEvent, SerializerSettings.Instance),
            integrationEvent.OccuredOnUtc);

        await _unitOfWork.GetWriteRepository<TMessage>()
            .CreateAsync(inboxMessage);

        await _unitOfWork.CommitAsync();
    }
}
