using Futions.CRM.Common.Application.EventBus;

namespace Futions.CRM.Modules.Catalogue.IntegrationEvents.Product;
public sealed class ProductRemovedFromProductBookIntegrationEvent : IntegrationEvent
{
    public ProductRemovedFromProductBookIntegrationEvent(
        Guid id, 
        DateTime occuredOnUtc,
        Guid productBookId,
        Guid productId) : base(id, occuredOnUtc)
    {
        ProductBookId = productBookId;
        ProductId = productId;
    }

    public Guid ProductBookId { get; private set; }
    public Guid ProductId { get; private set; }
}
