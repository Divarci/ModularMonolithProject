using Futions.CRM.Common.Application.Messaging;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Deals.Domain.Abstractions;
using Futions.CRM.Modules.Deals.Domain.Deals;

namespace Futions.CRM.Modules.Deals.Application.Deals.Commands.CreateDeal;
internal sealed class CreateDealCommandHandler(
    IDealsUnitOfWork unitOfWork)
    : ICommandHandler<CreateDealCommand, Deal>
{
    private readonly IDealsUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<Deal>> Handle(
        CreateDealCommand request, CancellationToken cancellationToken)
    {
        Result<Deal> result = Deal.Create(request.Title);

        if (result.IsFailure)
        {
            return Result.Failure<Deal>(result.Error);
        }

        await _unitOfWork
            .GetWriteRepository<Deal>()
            .CreateAsync(result.Value, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Success(result.Value);
    }
}
