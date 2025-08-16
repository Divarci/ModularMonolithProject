using Futions.CRM.Modules.Catalogue.Domain.Products;

namespace Futions.CRM.Modules.Catalogue.Domain.ProductBooks.Repositories;
public interface IProductBookWriteRepository
{
    Task<ProductBook> CreateProductBookAsync(
        string title,
        CancellationToken cancellationToken = default);

    Task<ProductBook> UpdateProductBookAsync(
        Guid productBookId, 
        string? title,
        bool? inactive,
        CancellationToken cancellationToken = default);

    Task DeleteProductBookAsync(
        Guid productBookId, 
        CancellationToken cancellationToken = default);

    Task<ProductBook> AddProductAsync(
            Guid productBookId,
            Product product,
            CancellationToken cancellationToken = default);

    Task<ProductBook> RemoveProductAsync(
        Guid productBookId,
        Guid productId,
        CancellationToken cancellationToken = default);

    Task<ProductBook> UpdateProductAsync(
        Guid productBookId,
        Guid productId,
        string? title,
        string? description,
        decimal? price,
        CancellationToken cancellationToken = default);
}
