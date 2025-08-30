using Futions.CRM.Common.Application.Messaging;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Deals.Domain.Abstractions;
using Futions.CRM.Modules.Deals.Domain.Deals;
using Futions.CRM.Modules.Deals.Domain.Deals.Errors;
using Microsoft.EntityFrameworkCore;

namespace Futions.CRM.Modules.Deals.Application.DealProducts.Commands.CreateDealProduct;
internal sealed class CreateDealProductCommandHandler(
    IDealsUnitOfWork unitOfWork)
    : ICommandHandler<CreateDealProductCommand, Guid>
{
    private readonly IDealsUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<Guid>> Handle(
        CreateDealProductCommand request, CancellationToken cancellationToken)
    {
        Deal deal = await _unitOfWork
            .GetReadRepository<Deal>()
            .Query(q=>q
                .Include(x=>x.DealProducts)
                .SingleOrDefaultAsync(x=>x.Id == request.DealId, cancellationToken));

        if (deal is null)
        {
            return Result.Failure<Guid>(DealErrors.NotFound(request.DealId));
        }

        Result<DealProduct> result = deal.AddProductToDealProducts(request.ProductId, request.Quantity, 
            request.Description, request.Price, request.Discount);

        if(result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        _unitOfWork.GetWriteRepository<Deal>()
            .Update(deal);

        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Success(result.Value.Id);
    }
}
