using Futions.CRM.Common.Application.EventBus;

namespace Futions.CRM.Modules.Catalogue.IntegrationEvents;
public sealed class ProductBookTitleUpdatedIntegrationEvent : IntegrationEvent
{
    public ProductBookTitleUpdatedIntegrationEvent(
        Guid id,
        DateTime occuredOnUtc,
        Guid productBookId,
        string title) : base(id, occuredOnUtc)
    {
        ProductBookId = productBookId;
        Title = title;
    }

    public Guid ProductBookId { get; init; }
    public string Title { get; init; }
}
