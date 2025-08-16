using Futions.CRM.Common.Domain.Results;

namespace Futions.CRM.Modules.Catalogue.Domain.ProductBooks;
public static class ProductBookErrors
{
    public static Error NotFound(Guid productBookId) => Error.NotFound(
        "ProductBook.NotFound",
        $"Product book with ID '{productBookId}' was not found.");

    public static readonly Error HasProducts = Error.Conflict(
        "ProductBook.HasProducts",
        "Cannot delete product book because it contains products.");

    public static readonly Error IsActive = Error.Conflict(
        "ProductBook.IsActive",
        "Product book is active.");

    public static readonly Error IsInactive = Error.Conflict(
        "ProductBook.IsInactive",
        "Product book is inactive.");

    public static Error MaxLength(string fieldName, int maxLength) => Error.Validation(
        "ProductBook.MaxLength",
        $"{fieldName} cannot be longer than {maxLength} characters.");

    public static Error NullValue(string fieldName) => Error.Validation(
        "ProductBook.NullValue",
        $"{fieldName} cannot be null or empty.");
}
