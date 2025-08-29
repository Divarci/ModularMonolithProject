using Futions.CRM.Common.Application.Messaging;
using Futions.CRM.Common.Domain.IUnitOfWorks;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Deals.Domain.Deals;
using Futions.CRM.Modules.Deals.Domain.Deals.Errors;
using Microsoft.EntityFrameworkCore;

namespace Futions.CRM.Modules.Deals.Application.DealProducts.Commands.CreateDealProduct;
internal sealed class CreateDealProductCommandHandler(
    IUnitOfWork unitOfWork) : ICommandHandler<CreateDealProductCommand, DealProduct>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<DealProduct>> Handle(
        CreateDealProductCommand request, CancellationToken cancellationToken)
    {
        Deal deal = await _unitOfWork
            .GetReadRepository<Deal>()
            .Query(q=>q
                .Include(x=>x.DealProducts)
                .SingleOrDefaultAsync(x=>x.Id == request.DealId, cancellationToken));

        if (deal is null)
        {
            return Result.Failure<DealProduct>(DealErrors.NotFound(request.DealId));
        }

        Result<DealProduct> result = deal.AddProductToDealProducts(request.ProductId, request.Quantity, 
            request.Description, request.Price, request.Discount);

        if(result.IsFailure)
        {
            return Result.Failure<DealProduct>(result.Error);
        }

        _unitOfWork.GetWriteRepository<Deal>()
            .Update(deal);

        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Success(result.Value);
    }
}
