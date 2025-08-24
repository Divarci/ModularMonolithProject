using Futions.CRM.Common.Domain.Results;

namespace Futions.CRM.Modules.Organisations.Domain.Organisations.Errors;
public static partial class OrganisationErrors
{
    public static Error NotFound(Guid organisationId) => Error.NotFound(
       "Organisation.NotFound",
       $"Organisation with ID '{organisationId}' was not found.");  

    public static Error NullValue(string fieldName) => Error.Validation(
        "Organisation.NullValue",
        $"{fieldName} cannot be null or empty.");
}
