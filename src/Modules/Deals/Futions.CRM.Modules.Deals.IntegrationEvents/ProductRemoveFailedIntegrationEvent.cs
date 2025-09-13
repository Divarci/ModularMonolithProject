using Futions.CRM.Common.Application.EventBus;

namespace Futions.CRM.Modules.Deals.IntegrationEvents;
public class ProductRemoveFailedIntegrationEvent : IntegrationEvent
{
    public ProductRemoveFailedIntegrationEvent(
        Guid id,
        Guid productBookId,
        Guid productId,
        DateTime occuredOnUtc) : base(id, occuredOnUtc)
    {
        ProductId = productId;
        ProductBookId = productBookId;
    }

    public Guid ProductBookId { get; set; }
    public Guid ProductId { get; set; }
}
