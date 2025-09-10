using Futions.CRM.Common.Application.EventBus;

namespace Futions.CRM.Modules.Catalogue.IntegrationEvents.Product;
public sealed class ProductCreatedIntegrationEvent : IntegrationEvent
{
    public ProductCreatedIntegrationEvent(
        Guid id,
        DateTime occuredOnUtc,
        Guid productBookId,
        Guid productId,
        string description,
        string title,
        decimal price) : base(id, occuredOnUtc)
    {
        ProductBookId = productBookId;
        Title = title;
        Description = description;
        Price = price;
        ProductId = productId;
    }

    public string Title { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public Guid ProductId { get; private set; }
    public Guid ProductBookId { get; private set; }
}
