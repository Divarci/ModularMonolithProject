using Futions.CRM.Common.Domain.Results;

namespace Futions.CRM.Modules.Deals.Domain.Deals.Errors;
public static class DealProductErrors
{
    public static Error NotFound(Guid dealProduct) => Error.NotFound(
        "DealProduct.NotFound",
        $"Deal product with ID '{dealProduct}' was not found.");

    public static Error MaxLength(string fieldName, int maxLength) => Error.Validation(
        "DealProduct.MaxLength",
        $"{fieldName} cannot be longer than {maxLength} characters.");

    public static Error MinValue(string fieldName, int minValue) => Error.Validation(
        "DealProduct.MinValue",
        $"{fieldName} cannot be less than {minValue}.");

    public static Error NullValue(string fieldName) => Error.Validation(
        "DealProduct.NullValue",
        $"{fieldName} cannot be null or empty.");
}
