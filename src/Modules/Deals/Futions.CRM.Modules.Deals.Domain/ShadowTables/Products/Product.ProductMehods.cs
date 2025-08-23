using Futions.CRM.Common.Domain.Extensions;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Deals.Domain.ShadowTables.Products.Errors;

namespace Futions.CRM.Modules.Deals.Domain.ShadowTables.Products;
public partial class Product
{
    public static Result<Product> Create(
        Guid productBookId, string title, string description, decimal price)
    {
        if (productBookId == Guid.Empty)
        {
            return Result.Failure<Product>(ProductErrors.NullValue(nameof(productBookId)));
        }

        Result titleResult = title.Validate(nameof(title), 64, "Product");

        if (titleResult.IsFailure)
        {
            return Result.Failure<Product>(titleResult.Error);
        }

        Result descResult = description.Validate(nameof(description), 512, "Product");

        if (descResult.IsFailure)
        {
            return Result.Failure<Product>(descResult.Error);
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
        Result titleResult = title.Validate(nameof(title), 64, "Product");

        if (titleResult.IsFailure)
        {
            return Result.Failure<Product>(titleResult.Error);
        }

        Title = title;

        return Result.Success();
    }

    public Result UpdateDescription(string description)
    {
        Result descResult = description.Validate(nameof(description), 512, "Product");

        if (descResult.IsFailure)
        {
            return Result.Failure<Product>(descResult.Error);
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
