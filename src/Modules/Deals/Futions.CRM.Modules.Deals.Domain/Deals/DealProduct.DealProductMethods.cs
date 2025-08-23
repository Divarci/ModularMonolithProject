using Futions.CRM.Common.Domain.Extensions;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Deals.Domain.Deals.Errors;

namespace Futions.CRM.Modules.Deals.Domain.Deals;
public sealed partial class DealProduct
{
    public static Result<DealProduct> Create(
        Guid dealId,
        Guid productId,
        int quantity,
        string description,
        decimal price,
        decimal discount)
    {
        Result result = description.Validate(nameof(description), 512, "DealProduct");

        if (result.IsFailure)
        {
            return Result.Failure<DealProduct>(result.Error);
        }

        if (dealId == Guid.Empty)
        {
            return Result.Failure<DealProduct>(DealProductErrors.NullValue(nameof(dealId)));
        }

        if (productId == Guid.Empty)
        {
            return Result.Failure<DealProduct>(DealProductErrors.NullValue(nameof(productId)));
        }

        if (quantity <= 0)
        {
            return Result.Failure<DealProduct>(DealProductErrors.MinValue(nameof(quantity), 0));
        }

        if (price < 0)
        {
            return Result.Failure<DealProduct>(DealProductErrors.MinValue(nameof(price), 0));
        }

        if (discount < 0)
        {
            return Result.Failure<DealProduct>(DealProductErrors.MinValue(nameof(discount), 0));
        }

        var dealProduct = new DealProduct(productId, dealId,
            quantity, description, price, discount);

        // Raise domain event

        return Result.Success(dealProduct);
    }

    public Result UpdateQuantity(int quantity)
    {
        if (quantity <= 0)
        {
            return Result.Failure<DealProduct>(DealProductErrors.MinValue(nameof(quantity), 0));
        }

        Quantity = quantity;

        // Raise domain event

        return Result.Success();
    }

    public Result UpdateDescription(string description)
    {
        Result result = description.Validate(nameof(description), 512, "DealProduct");

        if (result.IsFailure)
        {
            return Result.Failure<DealProduct>(result.Error);
        }

        Description = description;

        // Raise domain event

        return Result.Success();
    }

    public Result UpdatePrice(decimal price)
    {
        if (price < 0)
        {
            return Result.Failure<DealProduct>(DealProductErrors.MinValue(nameof(price), 0));
        }

        Price = price;

        // Raise domain event

        return Result.Success();
    }

    public Result UpdateDiscount(decimal discount)
    {
        if (discount < 0)
        {
            return Result.Failure<DealProduct>(DealProductErrors.MinValue(nameof(discount), 0));
        }

        Discount = discount;

        // Raise domain event

        return Result.Success();
    }
}
