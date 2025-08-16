using Futions.CRM.Common.Domain.Exceptions;
using Futions.CRM.Modules.Catalogue.Domain.ProductBooks.DomainEvents;
using Futions.CRM.Modules.Catalogue.Domain.Products;

namespace Futions.CRM.Modules.Catalogue.Domain.ProductBooks;
public partial class ProductBook
{
    public void AddProduct(Product product)
    {
        if (product is null)
        {
            throw new DomainException(
                entityName: nameof(ProductBook),
                error: ProductBookErrors.NullValue(nameof(product)));
        }

        IsActiveOrThrow();

        if (_products.Any(x=>x.Id == product.Id))
        {
            return;
        }

        _products.Add(product);

        Raise(new ProductAddedToProductBookDomainEvent(Id, product.Id));
    }

    public void RemoveProduct(Guid productId)
    {
        IsActiveOrThrow();

        Product product = GetProductOrThrow(productId);

        _products.Remove(product);

        Raise(new ProductRemovedFromProductBookDomainEvent(Id, product.Id));
    }

    public void UpdateProductDescription(Guid productId, string description)
    {
        IsActiveOrThrow();

        Product product = GetProductOrThrow(productId);

        product.UpdateDescription(description);
    }

    public void UpdateProductTitle(Guid productId, string title)
    {
        IsActiveOrThrow();

        Product product = GetProductOrThrow(productId);

        product.UpdateTitle(title);
    }

    public void UpdateProductPrice(Guid productId, decimal price)
    {
        IsActiveOrThrow();

        Product product = GetProductOrThrow(productId);

        product.UpdatePrice(price);
    }

    private Product GetProductOrThrow(Guid productId)
    {
        return _products.SingleOrDefault(p => p.Id == productId) ??
            throw new DomainException(
                entityName: nameof(Product),
                error: ProductErrors.NotFound(productId));
    }

    private void IsActiveOrThrow()
    {
        if (Inactive)
        {
            throw new DomainException(
                entityName: nameof(ProductBook),
                error: ProductBookErrors.IsInactive);
        }
    }
}
