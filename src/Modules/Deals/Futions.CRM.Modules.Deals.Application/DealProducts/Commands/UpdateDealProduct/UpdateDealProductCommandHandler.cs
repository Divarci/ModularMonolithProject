using Futions.CRM.Common.Application.Messaging;
using Futions.CRM.Common.Domain.IUnitOfWorks;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Deals.Domain.Deals;
using Futions.CRM.Modules.Deals.Domain.Deals.Errors;
using Microsoft.EntityFrameworkCore;

namespace Futions.CRM.Modules.Deals.Application.DealProducts.Commands.UpdateDealProduct;
internal sealed class UpdateDealProductCommandHandler(
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateDealProductCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(
        UpdateDealProductCommand request, CancellationToken cancellationToken)
    {
        Deal deal = await _unitOfWork
            .GetReadRepository<Deal>()
            .Query(q => q
                .Include(x => x.DealProducts)
                .SingleOrDefaultAsync(x => x.Id == request.DealId, cancellationToken));

        if (deal is null)
        {
            return Result.Failure(DealErrors.NotFound(request.DealId));
        }

        if (!string.IsNullOrWhiteSpace(request.Description))
        {
            Result result = deal.UpdateProductDescription(request.DealProductId, request.Description);

            if (result.IsFailure)
            {
                return Result.Failure(result.Error);
            }
        }

        if (request.Quantity.HasValue)
        {
            Result result = deal.UpdateProductQuantity(request.DealProductId, request.Quantity.Value);
            if (result.IsFailure)
            {
                return Result.Failure(result.Error);
            }
        }

        if (request.Price.HasValue)
        {
            Result result = deal.UpdateProductPrice(request.DealProductId, request.Price.Value);
            if (result.IsFailure)
            {
                return Result.Failure(result.Error);
            }
        }

        if (request.Discount.HasValue)
        {
            Result result = deal.UpdateProductDiscount(request.DealProductId, request.Discount.Value);
            if (result.IsFailure)
            {
                return Result.Failure(result.Error);
            }
        }

        _unitOfWork
            .GetWriteRepository<Deal>()
            .Update(deal);

        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Success();
    }
}
