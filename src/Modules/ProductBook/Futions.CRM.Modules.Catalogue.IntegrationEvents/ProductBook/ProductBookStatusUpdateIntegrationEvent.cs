using Futions.CRM.Common.Application.EventBus;

namespace Futions.CRM.Modules.Catalogue.IntegrationEvents.ProductBook;
public sealed class ProductBookStatusUpdateIntegrationEvent : IntegrationEvent
{
    public ProductBookStatusUpdateIntegrationEvent(
        Guid id,
        DateTime occuredOnUtc,
        Guid productBookId,
        bool inactive) : base(id, occuredOnUtc)
    {
        ProductBookId = productBookId;
        Inactive = inactive;
    }

    public Guid ProductBookId { get; init; }
    public bool Inactive { get; init; }
}
