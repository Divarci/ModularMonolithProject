using Futions.CRM.Common.Domain.Results;

namespace Futions.CRM.Modules.Deals.Domain.Deals.Errors;
public static class DealErrors
{
    public static Error NotFound(Guid dealId) => Error.NotFound(
   "Deal.NotFound",
   $"Organisation with ID '{dealId}' was not found.");

    public static Error MaxLength(string fieldName, int maxLength) => Error.Validation(
        "Deal.MaxLength",
        $"{fieldName} cannot be longer than {maxLength} characters.");

    public static Error NullValue(string fieldName) => Error.Validation(
        "Deal.NullValue",
        $"{fieldName} cannot be null or empty.");
}
