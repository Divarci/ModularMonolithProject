using Futions.CRM.Common.Domain.Results;

namespace Futions.CRM.Modules.Deals.Domain.ShadowTables.Organisations.Errors;
public static partial class OrganisationPersonErrors
{
    public static Error NotFound(Guid organisationPersonId) => Error.NotFound(
        "OrganisationPerson.NotFound",
        $"Organisation person with ID '{organisationPersonId}' was not found.");  

    public static Error NullValue(string fieldName) => Error.Validation(
        "OrganisationPerson.NullValue",
        $"{fieldName} cannot be null or empty.");
}
