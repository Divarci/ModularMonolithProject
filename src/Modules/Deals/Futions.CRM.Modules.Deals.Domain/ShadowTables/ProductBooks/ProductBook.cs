using Futions.CRM.Common.Domain.Entities;

namespace Futions.CRM.Modules.Deals.Domain.ShadowTables.ProductBooks;

public sealed partial class ProductBook : BaseEntity, IRootAggregate
{
    private ProductBook() { }

    private ProductBook(Guid id, string title)
    {    
        Id = id;
        Title = title;
        Inactive = false;
    }

    public string Title { get; private set; }

    public bool Inactive { get; private set; }


    private readonly List<Product> _products = [];
    public IReadOnlyCollection<Product> Products => _products.AsReadOnly();
}
