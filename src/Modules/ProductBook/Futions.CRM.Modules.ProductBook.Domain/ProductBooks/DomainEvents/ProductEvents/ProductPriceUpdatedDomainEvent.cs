using Futions.CRM.Common.Domain.DomainEvents;

namespace Futions.CRM.Modules.Catalogue.Domain.ProductBooks.DomainEvents.ProductEvents;
public sealed class ProductPriceUpdatedDomainEvent(
    Guid productBookId,
    Guid productId) : DomainEvent
{
    public Guid ProductBookId { get; set; } = productBookId;
    public Guid ProductId { get; set; } = productId;

}
