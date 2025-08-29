using Futions.CRM.Common.Domain.Extensions;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Deals.Domain.Deals.Errors;

namespace Futions.CRM.Modules.Deals.Domain.Deals;
public sealed partial class Deal
{
    public Result<DealProduct> AddProductToDealProducts(Guid productId, int quantity,
        string description, decimal price, decimal discount)
    {
        Result descriptionResult = description.Validate(nameof(description), 512, "Deal Product");

        if(descriptionResult.IsFailure)
        {
            return Result.Failure<DealProduct>(descriptionResult.Error);
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

        Result<DealProduct> result = DealProduct.Create(Id, productId,
            quantity, description, price, discount);

        if (result.IsFailure)
        {
            return Result.Failure<DealProduct>(result.Error);
        }

        _dealProducts.Add(result.Value);

        //Raise domain event

        return Result.Success(result.Value);
    }

    public Result RemoveProductFromDealProduct(Guid dealProductId)
    {
        if (dealProductId == Guid.Empty)
        {
            return Result.Failure<DealProduct>(DealProductErrors.NullValue(nameof(dealProductId)));
        }

        DealProduct? dealProduct = _dealProducts
            .SingleOrDefault(dp => dp.Id == dealProductId);

        if (dealProduct is null)
        {
            return Result.Failure<DealProduct>(DealProductErrors.NotFound(dealProductId));
        }

        _dealProducts.Remove(dealProduct);

        //Raise domain event

        return Result.Success();
    }

    public Result UpdateProductQuantity(Guid dealProductId, int quantity)
    {
        DealProduct dealProduct = _dealProducts
            .SingleOrDefault(dp => dp.Id == dealProductId);

        if (dealProduct is null)
        {
            return Result.Failure<DealProduct>(DealProductErrors.NotFound(dealProductId));
        }

        Result result = dealProduct.UpdateQuantity(quantity);

        if(result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        return Result.Success();
    }

    public Result UpdateProductDescription(Guid dealProductId, string description)
    {
        DealProduct dealProduct = _dealProducts
            .SingleOrDefault(dp => dp.Id == dealProductId);

        if (dealProduct is null)
        {
            return Result.Failure<DealProduct>(DealProductErrors.NotFound(dealProductId));
        }

        Result result = dealProduct.UpdateDescription(description);

        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        return Result.Success();
    }

    public Result UpdateProductPrice(Guid dealProductId, decimal price)
    {
        DealProduct dealProduct = _dealProducts
            .SingleOrDefault(dp => dp.Id == dealProductId);

        if (dealProduct is null)
        {
            return Result.Failure<DealProduct>(DealProductErrors.NotFound(dealProductId));
        }

        Result result = dealProduct.UpdatePrice(price);

        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        return Result.Success();
    }

    public Result UpdateProductDiscount(Guid dealProductId, decimal discount)
    {
        DealProduct dealProduct = _dealProducts
            .SingleOrDefault(dp => dp.Id == dealProductId);

        if (dealProduct is null)
        {
            return Result.Failure<DealProduct>(DealProductErrors.NotFound(dealProductId));
        }

        Result result = dealProduct.UpdateDiscount(discount);

        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        return Result.Success();
    }
}
