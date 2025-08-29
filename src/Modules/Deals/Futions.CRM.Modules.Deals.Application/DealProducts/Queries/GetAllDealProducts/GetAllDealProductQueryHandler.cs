using Futions.CRM.Common.Application.Messaging;
using Futions.CRM.Common.Domain.IUnitOfWorks;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Deals.Domain.Deals;
using Microsoft.EntityFrameworkCore;

namespace Futions.CRM.Modules.Deals.Application.DealProducts.Queries.GetAllDealProducts;
internal sealed class GetAllDealProductQueryHandler(
    IUnitOfWork unitOfWork) : IQueryHandler<GetAllDealProductQuery, DealProductDto[]>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<DealProductDto[]>> Handle(
        GetAllDealProductQuery request, CancellationToken cancellationToken)
    {
        DealProductDto[] dealProducts = await _unitOfWork
            .GetReadRepository<DealProduct>()
            .Query(q => q
                .Where(x => x.DealId == request.DealId)
                .Select(x => new DealProductDto
                {
                    Id = x.Id,
                    Description = x.Description,
                    Quantity = x.Quantity,
                    Price = x.Price,
                    Discount = x.Discount,
                    Title = x.Product.Title
                })
            .ToArrayAsync(cancellationToken));

        return Result.Success(dealProducts);
    }
}
