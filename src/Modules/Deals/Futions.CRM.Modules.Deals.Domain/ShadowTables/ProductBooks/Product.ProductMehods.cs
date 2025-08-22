using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Deals.Domain.ShadowTables.ProductBooks.Errors;

namespace Futions.CRM.Modules.Deals.Domain.ShadowTables.ProductBooks;
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

        return Result.Success();
    }

    public Result UpdatePrice(decimal price)
    {
        if (price < 0)
        {
            return Result.Failure<Product>(ProductErrors.NegativeValue(nameof(price)));
        }

        Price = price;

        return Result.Success();
    }
}
