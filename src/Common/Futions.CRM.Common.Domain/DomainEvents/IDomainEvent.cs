using MediatR;

namespace Futions.CRM.Common.Domain.DomainEvents;
public interface IDomainEvent
{
    Guid Id { get; }

    DateTime OccurredOnUtc { get; }
}
