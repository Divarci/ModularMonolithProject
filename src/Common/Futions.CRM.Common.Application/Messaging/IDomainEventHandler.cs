using Futions.CRM.Common.Domain.DomainEvents;
using MediatR;

namespace Futions.CRM.Common.Application.Messaging;

//public interface IDomainEventHandler
//{
//    Task Handle(IDomainEvent domainEvent, CancellationToken cancellationToken = default);
//}

public interface IDomainEventHandler<in TDomainEvent> : INotificationHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent;
//{
//    Task Handle(TDomainEvent domainEvent, CancellationToken cancellationToken = default);
//}
