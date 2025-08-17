using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Catalogue.Domain.Products.DomainEvents;

namespace Futions.CRM.Modules.Catalogue.Domain.Products;
public partial class Product
{
    public static Result<Product> Create(
        Guid productBookId, string title, string description, decimal price)
    {
        if (productBookId == Guid.Empty)
        {
            return Result.Failure<Product>(ProductErrors.NullValue(nameof(productBookId)));
        }

        if (string.IsNullOrWhiteSpace(title))
        {
            return Result.Failure<Product>(ProductErrors.NullValue(nameof(title)));
        }

        if (title.Length > 64)
        {
            return Result.Failure<Product>(ProductErrors.MaxLength(nameof(title), 64));
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            return Result.Failure<Product>(ProductErrors.NullValue(nameof(description)));
        }

        if (description.Length > 512)
        {
            return Result.Failure<Product>(ProductErrors.MaxLength(nameof(description), 512));
        }

        if (price < 0)
        {
            return Result.Failure<Product>(ProductErrors.NegativeValue(nameof(price)));
        }

        var product = new Product(productBookId, title, description, price);

        product.Raise(new ProductCreatedDomainEvent(productBookId, product.Id));

        return Result.Success(product);
    }

    public Result UpdateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return Result.Failure<Product>(ProductErrors.NullValue(nameof(title)));
        }

        if (title.Length > 64)
        {
            return Result.Failure<Product>(ProductErrors.MaxLength(nameof(title), 64));
        }

        Title = title;

        Raise(new ProductTitleUpdatedDomainEvent(ProductBookId, Id));

        return Result.Success();
    }

    public Result UpdateDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            return Result.Failure<Product>(ProductErrors.NullValue(nameof(description)));
        }

        if (description.Length > 512)
        {
            return Result.Failure<Product>(ProductErrors.MaxLength(nameof(description), 512));
        }

        Description = description;

        Raise(new ProductDescriptionUpdatedDomainEvent(ProductBookId, Id));

        return Result.Success();
    }

    public Result UpdatePrice(decimal price)
    {
        if (price < 0)
        {
            return Result.Failure<Product>(ProductErrors.NegativeValue(nameof(price)));
        }
               
        Price = price;

        Raise(new ProductPriceUpdatedDomainEvent(ProductBookId, Id));

        return Result.Success();
    }
}
