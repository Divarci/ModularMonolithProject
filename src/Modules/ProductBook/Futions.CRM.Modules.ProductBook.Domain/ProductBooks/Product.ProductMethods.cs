using Futions.CRM.Common.Domain.Abstractions.Entities.Extensions;
using Futions.CRM.Common.Domain.Exceptions;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Catalogue.Domain.ProductBooks.DomainEvents.ProductEvents;
using Futions.CRM.Modules.Catalogue.Domain.ProductBooks.Errors;

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

        product.Raise(new ProductCreatedDomainEvent(productBookId, product.Id));

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

        Raise(new ProductUpdatedDomainEvent(ProductBookId, Id));

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

        Raise(new ProductUpdatedDomainEvent(ProductBookId, Id));

        return Result.Success();
    }

    public Result UpdatePrice(decimal price)
    {
        if (price < 0)
        {
            return Result.Failure<Product>(ProductErrors.NegativeValue(nameof(price)));
        }

        Price = price;

        Raise(new ProductUpdatedDomainEvent(ProductBookId, Id));

        return Result.Success();
    }

    public Result IncraseDealCount()
    {
        ActiveDealCount++;

        return Result.Success();
    }

    public Result DecreaseDealCount()
    {
        if (ActiveDealCount == 0)
        {
            return Result.Failure<Product>(ProductErrors.DealCountZero);
        }

        ActiveDealCount++;

        return Result.Success();
    }

    public Result CheckProductIfCanbeRemoved()
    {
        bool isRemovable = ActiveDealCount <= 0;

        if (!isRemovable)
        {
            return Result.Failure(
                Error.Problem("Error.Product", "Product is not removable"));
        }

        IsPending = true;

        Raise(new CheckProductIfCanbeRemovedDomainEvent(ProductBookId, Id));

        return Result.Success();
    }

    public Result RemovePending()
    {
        IsPending = false;

        return Result.Success();
    }
}
