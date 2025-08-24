using Futions.CRM.Common.Domain.Extensions;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Deals.Domain.Deals.Errors;

namespace Futions.CRM.Modules.Deals.Domain.Deals;
public partial class Deal
{
    public static Result<Deal> Create(string title)
    {
        Result result = title.Validate(nameof(title), 64, "Deal");

        if (result.IsFailure)
        {
            return Result.Failure<Deal>(result.Error);
        }        

        var deal = new Deal(title);

        return Result.Success(deal);
    }

    public Result UpdateDealTitle(string title)
    {
        Result result = title.Validate(nameof(title), 64, "Deal");

        if (result.IsFailure)
        {
            return Result.Failure<Deal>(result.Error);
        }

        Title = title;

        return Result.Success();
    }

    public Result CloseDeal(DealStatus dealStatus)
    {
        if(dealStatus == DealStatus.Open)
        {
            return Result.Failure(DealErrors.DealIsClosed);
        }

        DealStatus = dealStatus;

        // Raise domain event

        return Result.Success();
    }

    public Result ActivateDeal(DealStatus dealStatus)
    {
        if (dealStatus == DealStatus.Won || dealStatus == DealStatus.Lost)
        {
            return Result.Failure(DealErrors.DealIsActive);
        }

        DealStatus = dealStatus;

        // Raise domain event

        return Result.Success();
    }
}
