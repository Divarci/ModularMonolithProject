using Futions.CRM.Common.Application.EventBus;

namespace Futions.CRM.Modules.Catalogue.IntegrationEvents.ProductBook;
public sealed class ProductBookRemovedIntegrationEvent : IntegrationEvent
{
    public ProductBookRemovedIntegrationEvent(
        Guid id, 
        DateTime occuredOnUtc,
        Guid productBookId) : base(id, occuredOnUtc)
    {
        ProductBookId = productBookId;
    }

    public Guid ProductBookId { get; init; }
}
