using Futions.CRM.Common.Domain.Abstractions.Entities;
using Futions.CRM.Modules.Catalogue.Domain.ProductBooks;

namespace Futions.CRM.Modules.Catalogue.Domain.Products;
public sealed partial class Product : BaseEntity, IAggregate
{
    private Product() { }

    private Product(Guid productBookId, string title, string description, decimal price)
    {
        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        Price = price;
        ProductBookId = productBookId;
        ActiveDealCount = 0;
    }

    public string Title { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public int ActiveDealCount { get; private set; }
    public bool IsPending { get; private set; }

    public Guid ProductBookId { get; private set; }
    public ProductBook ProductBook { get; private set; }
}
