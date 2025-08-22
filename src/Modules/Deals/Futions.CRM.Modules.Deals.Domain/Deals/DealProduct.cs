using Futions.CRM.Modules.Deals.Domain.ShadowTables.ProductBooks;

namespace Futions.CRM.Modules.Deals.Domain.Deals;
public sealed partial class DealProduct
{
    private DealProduct(Guid productId, Guid dealId,
        int quantity, string description, decimal price, decimal discount)
    {
        ProductId = productId;
        DealId = dealId;
        Quantity = quantity;
        Description = description;
        Price = price;
        Discount = discount;
    }

    public Guid ProductId { get; private set; }
    public Product Product { get; private set; }

    public Guid DealId { get; private set; }
    public Deal Deal { get; private set; }

    public int Quantity { get; private set; }
    public string Description { get; private set; }

    public decimal Price { get; private set; }
    public decimal Discount { get; private set; }
}
