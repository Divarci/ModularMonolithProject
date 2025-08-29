using Futions.CRM.Common.Domain.Results;

namespace Futions.CRM.Modules.Deals.Domain.ShadowTables.Products.Errors;
public static class ProductErrors
{
    public static Error NotFound(Guid productId) => Error.NotFound(
        "Product.NotFound",
        $"Product with ID '{productId}' was not found.");
   
    public static Error NullValue(string fieldName) => Error.Validation(
        "Product.NullValue",
        $"{fieldName} cannot be null or empty.");

    public static Error NegativeValue(string fieldName) => Error.Validation(
        "Product.NegativeValue",
        $"{fieldName} cannot be negative value.");

    public static Error HasDealProducts => Error.Validation(
        "Product.HasDealProducts",
        $"Product cannot be deleted because it is associated with existing deal products.");
}
