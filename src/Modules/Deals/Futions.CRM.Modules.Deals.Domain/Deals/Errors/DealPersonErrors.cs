using Futions.CRM.Common.Domain.Results;

namespace Futions.CRM.Modules.Deals.Domain.Deals.Errors;
public static class DealPersonErrors
{
    public static Error NotFound(Guid dealPersonId) => Error.NotFound(
   "DealPerson.NotFound",
   $"Organisation with ID '{dealPersonId}' was not found.");

    public static Error MaxLength(string fieldName, int maxLength) => Error.Validation(
        "DealPerson.MaxLength",
        $"{fieldName} cannot be longer than {maxLength} characters.");

    public static Error NullValue(string fieldName) => Error.Validation(
        "DealPerson.NullValue",
        $"{fieldName} cannot be null or empty.");

    public static readonly Error OnlyOneLeft = Error.Problem(
       "DealPerson.OnlyOneLeft",
       "Cannot delete deal person, because at least one deal person must be assigned to deal.");
}
