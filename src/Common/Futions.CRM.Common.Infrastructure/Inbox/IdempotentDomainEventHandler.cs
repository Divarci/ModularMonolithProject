using Futions.CRM.Common.Application.EventBus;
using Futions.CRM.Common.Domain.Abstractions.IUnitOfWorks;
using Futions.CRM.Common.Domain.Entities.MessageConsumers;
using Microsoft.EntityFrameworkCore;

namespace Futions.CRM.Common.Infrastructure.Inbox;
public sealed class IdempotentIntegrationEventHandler<TIntegrationEvent, TUnitOfWork, TMessageConsumer>(
    IIntegrationEventHandler<TIntegrationEvent> decorated,
    TUnitOfWork unitOfWork,
    IMessageConsumerFactory<TMessageConsumer> messageConsumerFactory) : IntegrationEventHandler<TIntegrationEvent>
    where TIntegrationEvent : IIntegrationEvent
    where TUnitOfWork : IUnitOfWork
    where TMessageConsumer : MessageConsumer
{
    private readonly IIntegrationEventHandler<TIntegrationEvent> _decorated = decorated;
    private readonly TUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMessageConsumerFactory<TMessageConsumer> _messageConsumerFactory = messageConsumerFactory;

    public override async Task Handle(TIntegrationEvent integrationEvent, CancellationToken cancellationToken = default)
    {
        TMessageConsumer inboxMessageConsumer = _messageConsumerFactory
            .Create(integrationEvent.Id, _decorated.GetType().Name);

        TMessageConsumer existingConsumer = await _unitOfWork
            .GetReadRepository<TMessageConsumer>()
            .GetAll()
            .SingleOrDefaultAsync(x => x.Id == inboxMessageConsumer.Id &&
                x.Name == inboxMessageConsumer.Name, cancellationToken);

        if (existingConsumer is not null)
        {
            return;
        }

        await _decorated.Handle(integrationEvent, cancellationToken);

        await _unitOfWork.GetWriteRepository<TMessageConsumer>()
            .CreateAsync(inboxMessageConsumer, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);
    }
}
