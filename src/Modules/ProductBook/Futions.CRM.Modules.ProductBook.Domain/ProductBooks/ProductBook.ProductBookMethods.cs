using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Catalogue.Domain.ProductBooks.DomainEvents;

namespace Futions.CRM.Modules.Catalogue.Domain.ProductBooks;
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

        productBook.Raise(new ProductBookCreatedDomainEvent(productBook.Id));

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

        Raise(new ProductBookTitleUpdatedDomainEvent(Id, Title));

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
