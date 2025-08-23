using Futions.CRM.Common.Domain.Extensions;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Deals.Domain.Deals.Errors;

namespace Futions.CRM.Modules.Deals.Domain.Deals;
public partial class Deal
{
    public static Result<Deal> Create(string title, Guid organisationId, Guid organisationPersonId)
    {
        Result result = title.Validate(nameof(title), 64, "Deal");

        if (result.IsFailure)
        {
            return Result.Failure<Deal>(result.Error);
        }

        if (organisationId == Guid.Empty)
        {
            return Result.Failure<Deal>(DealErrors.NullValue(nameof(organisationId)));
        }

        if (organisationPersonId == Guid.Empty)
        {
            return Result.Failure<Deal>(DealErrors.NullValue(nameof(organisationPersonId)));
        }

        var deal = new Deal(title, organisationId, organisationPersonId);

        //Raise domain event

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
}
