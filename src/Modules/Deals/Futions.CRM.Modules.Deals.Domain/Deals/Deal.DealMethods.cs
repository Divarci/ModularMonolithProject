using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Deals.Domain.Deals.Errors;

namespace Futions.CRM.Modules.Deals.Domain.Deals;
public partial class Deal
{
    public static Result<Deal> Create(string title, Guid organisationId, Guid organisationPersonId)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return Result.Failure<Deal>(DealErrors.NullValue(nameof(title)));
        }

        if (title.Length > 64)
        {
            return Result.Failure<Deal>(DealErrors.MaxLength(nameof(title),64));
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

        return Result.Success(deal);
    }

    public Result UpdateDealTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return Result.Failure(DealErrors.NullValue(nameof(title)));
        }

        if (title.Length > 64)
        {
            return Result.Failure(DealErrors.MaxLength(nameof(title), 64));
        }

        Title = title;

        return Result.Success();
    }
}
