using Futions.CRM.Common.Domain.DomainEvents;

namespace Futions.CRM.Modules.Catalogue.Domain.ProductBooks.DomainEvents.ProductBookEvents;
public sealed class ProductBookTitleUpdatedDomainEvent(
    Guid productBookId) : DomainEvent
{
    public Guid ProductBookId { get; set; } = productBookId;
}
