using Futions.CRM.Common.Domain.Exceptions;
using Futions.CRM.Modules.Catalogue.Domain.ProductBooks.DomainEvents;

namespace Futions.CRM.Modules.Catalogue.Domain.ProductBooks;
public partial class ProductBook
{
    public static ProductBook Create(string title)
    {
        var productBook = new ProductBook(title);

        productBook.Raise(new ProductBookCreatedDomainEvent(productBook.Id));

        return productBook;
    }

    public void UpdateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new DomainException(
                entityName: nameof(ProductBook),
                error: ProductBookErrors.NullValue(nameof(title)));
        }

        if (Title == title)
        {
            return;
        }

        Title = title;

        Raise(new ProductBookTitleUpdatedDomainEvent(Id, Title));
    }

    public void SetInactive()
    {
        bool hasProducts = _products.Count > 0;

        if (hasProducts)
        {
            throw new DomainException(
                entityName: nameof(ProductBook),
                error: ProductBookErrors.HasProducts);
        }

        IsActiveOrThrow();

        Inactive = true;

        Raise(new ProductBookSetInactiveDomainEvent(Id));
    }

    public void SetActive()
    {
        if (!Inactive)
        {
            throw new DomainException(
                entityName: $"{nameof(ProductBook)}.{nameof(Inactive)}",
                error: ProductBookErrors.IsActive);
        }

        Inactive = false;

        Raise(new ProductBookSetActiveDomainEvent(Id));
    }
}
