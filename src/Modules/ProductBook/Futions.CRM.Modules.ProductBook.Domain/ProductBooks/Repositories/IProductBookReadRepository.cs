using Futions.CRM.Modules.Catalogue.Domain.Products;

namespace Futions.CRM.Modules.Catalogue.Domain.ProductBooks.Repositories;
public interface IProductBookReadRepository
{
    Task<ProductBook[]> GetProductBooksAsync(
       CancellationToken cancellationToken = default);

    Task<ProductBook> GetProductBookAsync(
        Guid productBookId,
        CancellationToken cancellationToken = default);

    Task<Product> GetProductByIdAsync(
        Guid id, 
        CancellationToken cancellationToken = default);

    Task<Product[]> GetProductsByProductBookIdAsync(
        Guid productBookId,
        CancellationToken cancellationToken = default);
}
