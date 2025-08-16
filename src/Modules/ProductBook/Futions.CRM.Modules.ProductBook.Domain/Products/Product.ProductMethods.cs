using Futions.CRM.Common.Domain.Exceptions;
using Futions.CRM.Modules.Catalogue.Domain.Products.DomainEvents;

namespace Futions.CRM.Modules.Catalogue.Domain.Products;
public partial class Product
{
    internal static Product Create(Guid productBookId, string title, string description, decimal price)
    {
        var product = new Product(productBookId, title, description, price);

        product.Raise(new ProductCreatedDomainEvent(productBookId, product.Id));

        return product;
    }

    internal void UpdateTitle(string title)
    {
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

        if (Title == title)
        {
            return;
        }

        Title = title;

        Raise(new ProductTitleUpdatedDomainEvent(ProductBookId, Id));

    }

    internal void UpdateDescription(string description)
    {
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

        if (Description == description)
        {
            return;
        }

        Description = description;

        Raise(new ProductDescriptionUpdatedDomainEvent(ProductBookId, Id));
    }

    internal void UpdatePrice(decimal price)
    {
        if (price < 0)
        {
            throw new DomainException(
                entityName: nameof(Product),
                error: ProductErrors.NegativeValue(nameof(price)));
        }

        if (Price == price)
        {
            return;
        }

        Price = price;

        Raise(new ProductPriceUpdatedDomainEvent(ProductBookId, Id));
    }
}
