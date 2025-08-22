using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Deals.Domain.Deals.Errors;

namespace Futions.CRM.Modules.Deals.Domain.Deals;
public partial class Deal
{
    public Result AddDealPerson(Guid organisationPersonId)
    {
        if (organisationPersonId == Guid.Empty)
        {
            return Result.Failure(DealErrors.NullValue(nameof(organisationPersonId)));
        }

        Result<DealPerson> result = DealPerson.Create(organisationPersonId, Id);

        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        _dealPeople.Add(result.Value);

        return Result.Success();
    }

    public Result RemoveDealPerson(Guid dealPersonId)
    {
        if (DealPeople.Count == 1)
        {
            return Result.Failure(DealPersonErrors.OnlyOneLeft);
        }

        if (dealPersonId == Guid.Empty)
        {
            return Result.Failure(DealErrors.NullValue(nameof(dealPersonId)));
        }

        DealPerson dealPerson = _dealPeople.SingleOrDefault(dp => dp.Id == dealPersonId);

        if (dealPerson is null)
        {
            return Result.Failure(DealPersonErrors.NotFound(dealPersonId));
        }        

        _dealPeople.Remove(dealPerson);

        if (dealPerson.Id == PrimaryDealPersonId)
        {
            DealPerson person = DealPeople.FirstOrDefault();

            if(person is null)
            {
                return Result.Failure(DealErrors.NullValue(nameof(PrimaryDealPersonId)));
            }

            PrimaryDealPersonId = dealPerson.Id;
        }

        return Result.Success();
    }
}
