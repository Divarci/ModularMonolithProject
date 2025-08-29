using Futions.CRM.Common.Application.Messaging;
using Futions.CRM.Common.Domain.IUnitOfWorks;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Deals.Application.Deals.Queries.Shared;
using Futions.CRM.Modules.Deals.Domain.Deals;
using Futions.CRM.Modules.Deals.Domain.Deals.Errors;
using Microsoft.EntityFrameworkCore;

namespace Futions.CRM.Modules.Deals.Application.Deals.Queries.GetDealById;
internal sealed class GetDealByIdQueryHandler(
    IUnitOfWork unitOfWork) : IQueryHandler<GetDealByIdQuery, DealDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<DealDto>> Handle(
        GetDealByIdQuery request, CancellationToken cancellationToken)
    {
        DealDto? deal = await _unitOfWork
            .GetReadRepository<Deal>()
            .Query(q => q
                .Where(x=>x.Id == request.DealId)
                .Select(x=> new DealDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    DealStatus = x.DealStatus
                })
                .SingleOrDefaultAsync(cancellationToken));

        if (deal is null)
        {
            return Result.Failure<DealDto>(DealErrors.NotFound(request.DealId));
        }

        return Result.Success(deal);
    }
}
