using Futions.CRM.Common.Application.EventBus;

namespace Futions.CRM.Modules.Catalogue.IntegrationEvents.Product;
public sealed class ProductUpdatedIntegrationEvent : IntegrationEvent
{
    public ProductUpdatedIntegrationEvent(
        Guid id,
        DateTime occuredOnUtc
        ,
        Guid productBookId,
        Guid productId,
        string title,
        string description,
        decimal price) : base(id, occuredOnUtc)
    {
        ProductBookId = productBookId;
        ProductId = productId;
        Title = title;
        Description = description;
        Price = price;
    }

    public Guid ProductBookId { get; private set; }
    public Guid ProductId { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
}
