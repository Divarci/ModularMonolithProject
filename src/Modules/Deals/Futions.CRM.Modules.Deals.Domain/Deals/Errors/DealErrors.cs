using Futions.CRM.Common.Domain.Results;

namespace Futions.CRM.Modules.Deals.Domain.Deals.Errors;
public static class DealErrors
{
    public static Error NotFound(Guid dealId) => Error.NotFound(
       "Deal.NotFound",
       $"Deal with ID '{dealId}' was not found.");   

    public static Error NullValue(string fieldName) => Error.Validation(
        "Deal.NullValue",
        $"{fieldName} cannot be null or empty.");
}
