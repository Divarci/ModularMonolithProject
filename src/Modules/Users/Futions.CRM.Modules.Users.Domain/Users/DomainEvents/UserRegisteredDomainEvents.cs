using Futions.CRM.Common.Domain.DomainEvents;

namespace Futions.CRM.Modules.Users.Domain.Users.DomainEvents;
public sealed class UserRegisteredDomainEvents(
    Guid userId) : DomainEvent
{
    public Guid UserId { get; set; } = userId;
}   
