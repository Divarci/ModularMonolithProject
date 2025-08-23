using Futions.CRM.Common.Domain.Extensions;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Deals.Domain.ShadowTables.Organisations.Errors;

namespace Futions.CRM.Modules.Deals.Domain.ShadowTables.Organisations;
public sealed partial class OrganisationPerson
{
    public static Result<OrganisationPerson> Create(
        Guid organisationId,
        Guid personId,
        string personFulname)
    {
        Result  result = personFulname.Validate(nameof(personFulname), 128 , "OrganisationPerson");

        if (result.IsFailure)
        {
            return Result.Failure<OrganisationPerson>(result.Error);
        }

        if (organisationId == Guid.Empty)
        {
            return Result.Failure<OrganisationPerson>(OrganisationErrors.NullValue(nameof(organisationId)));
        }

        if (personId == Guid.Empty)
        {
            return Result.Failure<OrganisationPerson>(OrganisationPersonErrors.NullValue(nameof(personId)));
        }

        var organisationPerson = new OrganisationPerson(organisationId, personId, personFulname);

        return Result.Success(organisationPerson) ;
    }
}
