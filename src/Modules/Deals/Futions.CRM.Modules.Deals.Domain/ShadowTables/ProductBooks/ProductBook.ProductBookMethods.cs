using Futions.CRM.Common.Domain.Extensions;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Deals.Domain.ShadowTables.ProductBooks.Errors;

namespace Futions.CRM.Modules.Deals.Domain.ShadowTables.ProductBooks;
public partial class ProductBook
{
    public static Result<ProductBook> Create(Guid id, string title)
    {
        Result result = title.Validate(nameof(title), 64, "ProductBook");
        
        if (result.IsFailure)
        {
            return Result.Failure<ProductBook>(result.Error);
        }

        var productBook = new ProductBook(id, title);

        return Result.Success(productBook);
    }

    public Result UpdateTitle(string title)
    {
        Result result = title.Validate(nameof(title), 64, "ProductBook");

        if (result.IsFailure)
        {
            return Result.Failure<ProductBook>(result.Error);
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
