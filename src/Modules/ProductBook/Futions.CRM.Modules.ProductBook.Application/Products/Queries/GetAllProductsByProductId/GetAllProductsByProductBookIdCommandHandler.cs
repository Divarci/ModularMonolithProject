using Futions.CRM.Common.Application.Messaging;
using Futions.CRM.Common.Domain.IUnitOfWorks;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Catalogue.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace Futions.CRM.Modules.Catalogue.Application.Products.Queries.GetAllProductsByProductId;
internal sealed class GetAllProductsByProductBookIdCommandHandler(
    IUnitOfWork unitOfWork) : IQueryHandler<GetAllProductsByProductBookIdCommand, ProductDto[]>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<ProductDto[]>> Handle(
        GetAllProductsByProductBookIdCommand request, CancellationToken cancellationToken)
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
