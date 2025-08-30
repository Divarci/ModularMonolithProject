using Futions.CRM.Common.Application.Messaging;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Deals.Domain.Abstractions;
using Futions.CRM.Modules.Deals.Domain.ShadowTables.Products;
using Microsoft.EntityFrameworkCore;

namespace Futions.CRM.Modules.Deals.Application.Products.Queries.GetAllProductByProductBookId;
internal sealed class GetAllProductsByProductBookIdQueryHandler(
    IDealsUnitOfWork unitOfWork) : IQueryHandler<GetAllProductsByProductBookIdQuery, ProductDto[]>
{
    private readonly IDealsUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<ProductDto[]>> Handle(
        GetAllProductsByProductBookIdQuery request, CancellationToken cancellationToken)
    {
        ProductDto[] products = await _unitOfWork
            .GetReadRepository<Product>()
            .Query(query => query
                .Where(x => x.ProductBookId == request.ProductBookId)
                .Select(x => new ProductDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Price = x.Price
                })
                .ToArrayAsync(cancellationToken));

        return Result.Success(products);
    }
}
