using Futions.CRM.Common.Domain.Abstractions.Entities.Extensions;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Catalogue.Domain.ProductBooks.DomainEvents.ProductBookEvents;
using Futions.CRM.Modules.Catalogue.Domain.ProductBooks.Errors;

namespace Futions.CRM.Modules.Catalogue.Domain.ProductBooks;
public partial class ProductBook
{
    public static Result<ProductBook> Create(string title)
    {
        Result result = title.Validate(nameof(title), 64, "ProductBook");
        
        if (result.IsFailure)
        {
            return Result.Failure<ProductBook>(result.Error);
        }

        var productBook = new ProductBook(title);

        productBook.Raise(new ProductBookCreatedDomainEvent(productBook.Id));

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

        Raise(new ProductBookSetInactiveDomainEvent(Id));

        return Result.Success();
    }

    public Result SetActive()
    {
        if (!Inactive)
        {
            return Result.Failure(ProductBookErrors.IsActive);
        }

        Inactive = false;

        Raise(new ProductBookSetActiveDomainEvent(Id));

        return Result.Success();
    }    
}
