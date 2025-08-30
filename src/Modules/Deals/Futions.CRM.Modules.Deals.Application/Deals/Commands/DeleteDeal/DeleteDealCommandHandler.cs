using Futions.CRM.Common.Application.Messaging;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Deals.Domain.Abstractions;
using Futions.CRM.Modules.Deals.Domain.Deals;
using Futions.CRM.Modules.Deals.Domain.Deals.Errors;
using Microsoft.EntityFrameworkCore;

namespace Futions.CRM.Modules.Deals.Application.Deals.Commands.DeleteDeal;
internal sealed class DeleteDealCommandHandler(
    IDealsUnitOfWork unitOfWork) 
    : ICommandHandler<DeleteDealCommand>
{
    private readonly IDealsUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(
        DeleteDealCommand request, CancellationToken cancellationToken)
    {
        Deal deal = await _unitOfWork
             .GetReadRepository<Deal>()
             .Query(query => query
                 .Include(x => x.DealProducts)
                 .SingleOrDefaultAsync(x => x.Id == request.DealId, cancellationToken)
             );

        if (deal is null)
        {
            return Result.Failure(DealErrors.NotFound(request.DealId));
        }       

        _unitOfWork.GetWriteRepository<Deal>().Delete(deal);

        await _unitOfWork.CommitAsync(cancellationToken);

        // Raise domain event

        return Result.Success();
    }
}
