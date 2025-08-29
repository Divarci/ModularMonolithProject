using Futions.CRM.Common.Application.Messaging;
using Futions.CRM.Common.Domain.IUnitOfWorks;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Deals.Application.Deals.Queries.Shared;
using Futions.CRM.Modules.Deals.Domain.Deals;
using Microsoft.EntityFrameworkCore;

namespace Futions.CRM.Modules.Deals.Application.Deals.Queries.GetAllDeals;
internal sealed class GetAllDealsQueryHandler(
    IUnitOfWork unitOfWork) : IQueryHandler<GetAllDealsQuery, DealDto[]>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<DealDto[]>> Handle(
        GetAllDealsQuery request, CancellationToken cancellationToken)
    {
        DealDto[] deals = await _unitOfWork
            .GetReadRepository<Deal>()
            .GetAll()
            .Select(deal => new DealDto {
                Id = deal.Id,
                Title = deal.Title,
                DealStatus = deal.DealStatus
            })
            .ToArrayAsync(cancellationToken);

        return Result.Success(deals);
    }
}
