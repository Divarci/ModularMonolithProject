using Futions.CRM.Common.Domain;
using Futions.CRM.Common.Domain.Exceptions;
using Futions.CRM.Modules.Catalogue.Domain.ProductBooks.DomainEvents;
using Futions.CRM.Modules.Catalogue.Domain.Products;

namespace Futions.CRM.Modules.Catalogue.Domain.ProductBooks;

public sealed partial class ProductBook : BaseEntity
{
    private ProductBook() { }
    private ProductBook(string title)
    {
        if(string.IsNullOrWhiteSpace(title))
        {
            throw new DomainException(
                entityName: nameof(ProductBook),
                error: ProductBookErrors.NullValue(nameof(title)));
        }

        if (title.Length > 64)
        {
            throw new DomainException(
                entityName: nameof(ProductBook),
                error: ProductBookErrors.MaxLength(nameof(title),64));
        }

        Title = title;
        Inactive = false;
    }

    public string Title { get; private set; }

    public bool Inactive { get; private set; }


    private readonly List<Product> _products = [];
    public IReadOnlyCollection<Product> Products => _products.AsReadOnly();    
}
