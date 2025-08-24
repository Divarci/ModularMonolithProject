using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Organisations.Domain.Organisations.Errors;

namespace Futions.CRM.Modules.Organisations.Domain.Organisations;
public sealed partial class OrganisationPerson
{
    public static Result<OrganisationPerson> Create(
        Guid organisationId,
        Guid personId)
    {
        if (organisationId == Guid.Empty)
        {
            return Result.Failure<OrganisationPerson>(OrganisationErrors.NullValue(nameof(organisationId)));
        }

        if (personId == Guid.Empty)
        {
            return Result.Failure<OrganisationPerson>(OrganisationPersonErrors.NullValue(nameof(personId)));
        }

        var organisationPerson = new OrganisationPerson(organisationId, personId);

        return Result.Success(organisationPerson) ;
    }
}
