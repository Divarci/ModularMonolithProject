using Futions.CRM.Common.Application.EventBus;

namespace Futions.CRM.Modules.Catalogue.IntegrationEvents.Product;
public sealed class CheckProductIfCanbeRemovedIntegrationEvent : IntegrationEvent
{
    public CheckProductIfCanbeRemovedIntegrationEvent(
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
