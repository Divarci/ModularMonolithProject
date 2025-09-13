using Futions.CRM.Common.Domain.DomainEvents;

namespace Futions.CRM.Modules.Catalogue.Domain.ProductBooks.DomainEvents.ProductEvents;
public sealed class CheckProductIfCanbeRemovedDomainEvent(
    Guid ProductBookId,
    Guid ProductId) : DomainEvent
{
    public Guid ProductBookId { get; set; } = ProductBookId;
    public Guid ProductId { get; set; } = ProductId;
}
