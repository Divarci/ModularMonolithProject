using Futions.CRM.Common.Domain.Entities;
using Futions.CRM.Modules.Catalogue.Domain.Products;

namespace Futions.CRM.Modules.Catalogue.Domain.ProductBooks;

public sealed partial class ProductBook : BaseEntity, IRootAggregate
{
    private ProductBook() { }
    private ProductBook(string title)
    {    
        Id = Guid.NewGuid();
        Title = title;
        Inactive = false;
    }

    public string Title { get; private set; }

    public bool Inactive { get; private set; }


    private readonly List<Product> _products = [];
    public IReadOnlyCollection<Product> Products => _products.AsReadOnly();
}
