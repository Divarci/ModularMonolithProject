using Futions.CRM.Common.Application.Messaging;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Catalogue.Application.Products.Queries.GetAllProductsByProductBookId;
using Futions.CRM.Modules.Catalogue.Domain.Abstractions;
using Futions.CRM.Modules.Catalogue.Domain.ProductBooks.Errors;
using Futions.CRM.Modules.Catalogue.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace Futions.CRM.Modules.Catalogue.Application.Products.Queries.GetProductById;
internal sealed class GetProductByIdQueryHandler(
    ICatalogueUnitOfWork unitOfWork) : IQueryHandler<GetProductByIdQuery, ProductDto>
{
    private readonly ICatalogueUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<ProductDto>> Handle(
        GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        Product product = await _unitOfWork.GetReadRepository<Product>()
            .GetAll()
            .SingleOrDefaultAsync(x=>x.Id == request.ProductId &&
                x.ProductBookId == request.ProductBookId, cancellationToken);

        if (product is null)
        {
            return Result.Failure<ProductDto>(
                ProductErrors.NotFound(request.ProductId));
        }

        return Result.Success(new ProductDto
        {
            Id = product.Id,
            Description = product.Description,
            Title = product.Title,
            Price = product.Price
        });
    }
}
