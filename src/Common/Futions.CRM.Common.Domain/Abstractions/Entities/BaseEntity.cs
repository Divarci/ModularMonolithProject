using Futions.CRM.Common.Domain.DomainEvents;

namespace Futions.CRM.Common.Domain.Abstractions.Entities;
public abstract class BaseEntity
{
    private readonly List<IDomainEvent> _domainEvents = [];

    protected BaseEntity() { }

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.ToList();

    public Guid Id { get; init; }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    protected void Raise(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}
