using Futions.CRM.Common.Application.Messaging;
using Futions.CRM.Common.Domain.Abstractions.IUnitOfWorks;
using Futions.CRM.Common.Domain.DomainEvents;
using Futions.CRM.Common.Domain.Entities.MessageConsumers;
using Microsoft.EntityFrameworkCore;

namespace Futions.CRM.Common.Infrastructure.MessageBox.Outbox;
public sealed class IdempotentDomainEventHandler<TDomainEvent, TUnitOfWork, TMessageConsumer>(
    IDomainEventHandler<TDomainEvent> decorated,
    TUnitOfWork unitOfWork,
    IMessageConsumerFactory<TMessageConsumer> messageConsumerFactory) : DomainEventHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent
    where TUnitOfWork : IUnitOfWork
    where TMessageConsumer : MessageConsumer
{
    private readonly IDomainEventHandler<TDomainEvent> _decorated = decorated;
    private readonly TUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMessageConsumerFactory<TMessageConsumer> _messageConsumerFactory = messageConsumerFactory;

    public override async Task Handle(TDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        TMessageConsumer outboxMessageConsumer = _messageConsumerFactory
            .Create(domainEvent.Id, _decorated.GetType().Name);

        TMessageConsumer existingConsumer = await _unitOfWork
            .GetReadRepository<TMessageConsumer>()
            .GetAll()
            .SingleOrDefaultAsync(x => x.Id == outboxMessageConsumer.Id &&
                x.Name == outboxMessageConsumer.Name, cancellationToken);

        if (existingConsumer is not null)
        {
            return;
        }

        await _decorated.Handle(domainEvent, cancellationToken);

        await _unitOfWork.GetWriteRepository<TMessageConsumer>()
            .CreateAsync(outboxMessageConsumer, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);
    }
}
