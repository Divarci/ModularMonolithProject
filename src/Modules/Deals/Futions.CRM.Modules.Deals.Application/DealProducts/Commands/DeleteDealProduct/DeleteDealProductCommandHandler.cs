using Futions.CRM.Common.Application.Messaging;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Deals.Domain.Abstractions;
using Futions.CRM.Modules.Deals.Domain.Deals;
using Futions.CRM.Modules.Deals.Domain.Deals.Errors;
using Microsoft.EntityFrameworkCore;

namespace Futions.CRM.Modules.Deals.Application.DealProducts.Commands.DeleteDealProduct;
internal sealed class DeleteDealProductCommandHandler(
    IDealsUnitOfWork unitOfWork)
    : ICommandHandler<DeleteDealProductCommand>
{
    private readonly IDealsUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(
        DeleteDealProductCommand request, CancellationToken cancellationToken)
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

        deal.RemoveProductFromDealProduct(request.DealProductId);

        _unitOfWork.GetWriteRepository<Deal>()
            .Update(deal);

        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Success();
    }
}
