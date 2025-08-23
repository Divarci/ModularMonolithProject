using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Catalogue.Domain.ProductBooks.DomainEvents;
using Futions.CRM.Modules.Catalogue.Domain.Products;

namespace Futions.CRM.Modules.Catalogue.Domain.ProductBooks;
public partial class ProductBook
{
    public Result AddProduct(Product product)
    {
        if (product is null)
        {
            return Result.Failure<ProductBook>(
                ProductErrors.NullValue(nameof(product)));
        }

        if (Inactive)
        {
            return Result.Failure(ProductBookErrors.IsInactive);
        }

        if (_products.Any(x => x.Id == product.Id))
        {
            return Result.Failure(ProductBookErrors.HasExist(product.Id.ToString()));
        }

        _products.Add(product);

        Raise(new ProductAddedToProductBookDomainEvent(Id, product.Id));

        return Result.Success();
    }

    public Result RemoveProduct(Guid productId)
    {
        if (Inactive)
        {
            return Result.Failure(ProductBookErrors.IsInactive);
        }

        Product product = _products.SingleOrDefault(p => p.Id == productId);

        if (product is null)
        {
            return Result.Failure(ProductErrors.NotFound(productId));
        }

        _products.Remove(product);

        Raise(new ProductRemovedFromProductBookDomainEvent(Id, product.Id));

        return Result.Success();
    }

    public Result UpdateProductDescription(Guid productId, string description)
    {
        if (Inactive)
        {
            return Result.Failure(ProductBookErrors.IsInactive);
        }

        Product product = _products.SingleOrDefault(p => p.Id == productId);

        if (product is null)
        {
            return Result.Failure(ProductErrors.NotFound(productId));
        }

        Result result = product.UpdateDescription(description);

        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        return Result.Success();
    }

    public Result UpdateProductTitle(Guid productId, string title)
    {
        if (Inactive)
        {
            return Result.Failure(ProductBookErrors.IsInactive);
        }

        Product product = _products.SingleOrDefault(p => p.Id == productId);

        if (product is null)
        {
            return Result.Failure(ProductErrors.NotFound(productId));
        }

        Result result = product.UpdateTitle(title);

        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        return Result.Success();
    }

    public Result UpdateProductPrice(Guid productId, decimal price)
    {
        if (Inactive)
        {
            return Result.Failure(ProductBookErrors.IsInactive);
        }

        Product product = _products.SingleOrDefault(p => p.Id == productId);

        if (product is null)
        {
            return Result.Failure(ProductErrors.NotFound(productId));
        }

        Result result = product.UpdatePrice(price);

        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        return Result.Success();
    }
}
