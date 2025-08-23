using Futions.CRM.Common.Domain.Results;

namespace Futions.CRM.Modules.Catalogue.Domain.Products;
public static class ProductErrors
{
    public static Error NotFound(Guid productId) => Error.NotFound(
        "Product.NotFound",
        $"Product with ID '{productId}' was not found.");   

    public static Error MaxLength(string fieldName, int maxLength) => Error.Validation(
        "Product.MaxLength",
        $"{fieldName} cannot be longer than {maxLength} characters.");

    public static Error NullValue(string fieldName) => Error.Validation(
        "Product.NullValue",
        $"{fieldName} cannot be null or empty.");

    public static Error NegativeValue(string fieldName) => Error.Validation(
        "Product.NegativeValue",
        $"{fieldName} cannot be negative value.");

    public static readonly Error DealCountZero = Error.Problem(
        "Product.DealCountZero",
        "Cannot Decrease deal count. Because current deal count is 0.");
}
