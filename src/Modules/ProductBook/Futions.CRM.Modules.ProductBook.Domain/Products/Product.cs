using Futions.CRM.Common.Domain;
using Futions.CRM.Common.Domain.Exceptions;
using Futions.CRM.Modules.Catalogue.Domain.ProductBooks;

namespace Futions.CRM.Modules.Catalogue.Domain.Products;
public sealed partial class Product : BaseEntity
{
    private Product() { }

    private Product(Guid productBookId, string title, string description, decimal price)
    {
        if (productBookId == Guid.Empty)
        {
            throw new DomainException(
                entityName: nameof(Product),
                error: ProductErrors.NullValue(nameof(productBookId)));
        }

        if (string.IsNullOrWhiteSpace(title))
        {
            throw new DomainException(
                entityName: nameof(Product),
                error: ProductErrors.NullValue(nameof(title)));
        }

        if (title.Length > 64)
        {
            throw new DomainException(
                entityName: nameof(Product),
                error: ProductErrors.MaxLength(nameof(title), 64));
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            throw new DomainException(
                entityName: nameof(Product),
                error: ProductErrors.NullValue(nameof(description)));
        }

        if (description.Length > 512)
        {
            throw new DomainException(
                entityName: nameof(Product),
                error: ProductErrors.MaxLength(nameof(description), 512));
        }

        if (price < 0)
        {
            throw new DomainException(
                entityName: nameof(Product),
                error: ProductErrors.NegativeValue(nameof(price)));
        }

        Title = title;
        Description = description;
        Price = price;
        ProductBookId = productBookId;
    }

    public string Title { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }

    public Guid ProductBookId { get; private set; }
    public ProductBook ProductBook { get; private set; }
}
