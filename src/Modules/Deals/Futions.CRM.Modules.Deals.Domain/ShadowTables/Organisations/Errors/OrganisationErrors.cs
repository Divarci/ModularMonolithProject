using Futions.CRM.Common.Domain.Results;

namespace Futions.CRM.Modules.Deals.Domain.ShadowTables.Organisations.Errors;
public static partial class OrganisationErrors
{
    public static Error NotFound(Guid organisationId) => Error.NotFound(
       "Organisation.NotFound",
       $"Organisation with ID '{organisationId}' was not found."); 

    public static Error MaxLength(string fieldName, int maxLength) => Error.Validation(
        "Organisation.MaxLength",
        $"{fieldName} cannot be longer than {maxLength} characters.");

    public static Error NullValue(string fieldName) => Error.Validation(
        "Organisation.NullValue",
        $"{fieldName} cannot be null or empty.");
}
