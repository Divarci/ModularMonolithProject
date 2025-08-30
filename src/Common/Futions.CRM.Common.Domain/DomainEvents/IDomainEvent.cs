using MediatR;

namespace Futions.CRM.Common.Domain.DomainEvents;
public interface IDomainEvent : INotification
{
    Guid Id { get; }

    DateTime OccurredOnUtc { get; }
}
