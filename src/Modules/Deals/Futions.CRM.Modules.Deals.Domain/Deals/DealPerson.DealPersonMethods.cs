using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Deals.Domain.Deals.Errors;

namespace Futions.CRM.Modules.Deals.Domain.Deals;
public sealed partial class DealPerson
{
    public static Result<DealPerson> Create(Guid organisationPersonId, Guid dealId)
    {
        if (organisationPersonId == Guid.Empty)
        {
            return Result.Failure<DealPerson>(DealPersonErrors.NullValue(nameof(organisationPersonId)));
            
        }

        if (dealId == Guid.Empty)
        {
            return Result.Failure<DealPerson>(DealPersonErrors.NullValue(nameof(dealId)));

        }

        var dealPerson = new DealPerson(organisationPersonId, dealId);

        //Raise domain event

        return Result.Success(dealPerson);
    }
}
