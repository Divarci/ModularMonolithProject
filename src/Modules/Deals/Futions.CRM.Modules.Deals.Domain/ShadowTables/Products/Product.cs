using Futions.CRM.Common.Domain.Entities;
using Futions.CRM.Modules.Deals.Domain.Deals;

namespace Futions.CRM.Modules.Deals.Domain.ShadowTables.Products;
public sealed partial class Product : BaseEntity, IAggregate
{
    private Product(Guid productBookId, string title, string description, decimal price)
    {
        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        Price = price;
        ProductBookId = productBookId;
    }

    public string Title { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }

    public Guid ProductBookId { get; private set; }

    private readonly List<DealProduct> _dealProducts = [];
    public IReadOnlyCollection<DealProduct> DealProducts => _dealProducts.AsReadOnly();
}
