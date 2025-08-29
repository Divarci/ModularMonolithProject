using Futions.CRM.Common.Application.Messaging;
using Futions.CRM.Common.Domain.IUnitOfWorks;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Deals.Domain.Deals;
using Futions.CRM.Modules.Deals.Domain.Deals.Errors;

namespace Futions.CRM.Modules.Deals.Application.Deals.Commands.UpdateDeal;
internal sealed class UpdateDealCommandHandler(
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateDealCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(
        UpdateDealCommand request, CancellationToken cancellationToken)
    {
        Deal? deal = await _unitOfWork
             .GetReadRepository<Deal>()
             .GetByIdAsync(request.DealId, cancellationToken);

        if (deal is null)
        {
            return Result.Failure(DealErrors.NotFound(request.DealId));
        }

        if (!string.IsNullOrWhiteSpace(request.Title))
        {
            deal.UpdateDealTitle(request.Title);
        }

        if (request.DealStatus.HasValue)
        {
            if (request.DealStatus == DealStatus.Open)
            {
                Result result = deal.ActivateDeal(request.DealStatus.Value);

                if (result.IsFailure)
                {
                    return Result.Failure(result.Error);
                }
            }
            else
            {
                Result result = deal.CloseDeal(request.DealStatus.Value);

                if (result.IsFailure)
                {
                    return Result.Failure(result.Error);
                }
            }
        }

        return Result.Success();
    }
}

