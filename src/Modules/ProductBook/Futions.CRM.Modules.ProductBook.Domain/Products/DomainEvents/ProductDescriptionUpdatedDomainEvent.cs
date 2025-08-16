using Futions.CRM.Common.Domain.DomainEvents;

namespace Futions.CRM.Modules.Catalogue.Domain.Products.DomainEvents;
public class ProductDescriptionUpdatedDomainEvent(
    Guid productBookId,
    Guid productId) : DomainEvent
{
    public Guid ProductBookId { get; set; } = productBookId;
    public Guid ProductId { get; set; } = productId;

}
