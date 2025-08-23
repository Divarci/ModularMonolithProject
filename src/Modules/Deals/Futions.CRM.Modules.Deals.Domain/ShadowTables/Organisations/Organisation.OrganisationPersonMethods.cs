using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Deals.Domain.ShadowTables.Organisations.Errors;

namespace Futions.CRM.Modules.Deals.Domain.ShadowTables.Organisations;
public sealed partial class Organisation
{
    public Result AddPersonToOrganisation(Guid personId, string personFullname)
    {
        if(personId == Guid.Empty)
        {
            return Result.Failure(OrganisationPersonErrors.NullValue(nameof(personId)));
        }

        Result<OrganisationPerson> result = OrganisationPerson.Create(Id, personId, personFullname);
        
        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        _organisationPeople.Add(result.Value);

        return Result.Success();
    }

    public Result RemovePersonFromOrganisation(Guid organisationPersonId)
    {
        OrganisationPerson organisationPerson = _organisationPeople
            .SingleOrDefault(op => op.Id == organisationPersonId);

        if (organisationPerson is null)
        {
            return Result.Failure(OrganisationPersonErrors.NotFound(organisationPersonId));
        }

        _organisationPeople.Remove(organisationPerson);

        return Result.Success();
    }
}
