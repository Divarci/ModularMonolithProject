using Futions.CRM.Common.Domain.Results;

namespace Futions.CRM.Modules.Organisations.Domain.ShadowTables.People.Errors;
public static class PersonError
{
    public static Error NotFound(Guid personId) => Error.NotFound(
        "Person.NotFound",
        $"Person with ID '{personId}' was not found.");

    public static Error MaxLength(string fieldName, int maxLength) => Error.Validation(
        "Person.MaxLength",
        $"{fieldName} cannot be longer than {maxLength} characters.");

    public static Error NullValue(string fieldName) => Error.Validation(
        "Person.NullValue",
        $"{fieldName} cannot be null or empty.");

}
