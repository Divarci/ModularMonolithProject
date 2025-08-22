using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Deals.Domain.ShadowTables.ProductBooks.Errors;

namespace Futions.CRM.Modules.Deals.Domain.ShadowTables.ProductBooks;
public partial class ProductBook
{
    public static Result<ProductBook> Create(string title)
    {

        if (string.IsNullOrWhiteSpace(title))
        {
            return Result.Failure<ProductBook>(
                ProductBookErrors.NullValue(nameof(title)));
        }

        if (title.Length > 64)
        {
            return Result.Failure<ProductBook>(
                ProductBookErrors.MaxLength(nameof(title), 64));
        }

        var productBook = new ProductBook(title);

        return Result.Success(productBook);
    }

    public Result UpdateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return Result.Failure(
                ProductBookErrors.NullValue(nameof(title)));
        }

        Title = title;

        return Result.Success();
    }

    public Result SetInactive()
    {
        bool hasProducts = _products.Count > 0;

        if (hasProducts)
        {
            return Result.Failure(ProductBookErrors.HasProducts);
        }

        if (Inactive)
        {
            return Result.Failure(ProductBookErrors.IsInactive);
        }

        Inactive = true;

        return Result.Success();
    }

    public Result SetActive()
    {
        if (!Inactive)
        {
            return Result.Failure(ProductBookErrors.IsActive);
        }

        Inactive = false;

        return Result.Success();
    }
}
