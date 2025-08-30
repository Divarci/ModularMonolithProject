using Futions.CRM.Common.Application.Messaging;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Deals.Domain.Abstractions;
using Futions.CRM.Modules.Deals.Domain.ShadowTables.ProductBooks;

namespace Futions.CRM.Modules.Deals.Application.ProductBooks.Commands.CreateProductBook;
internal sealed class CreateProductBookCommandHandler(
    IDealsUnitOfWork unitOfWOrk) : ICommandHandler<CreateProductBookCommand, Guid>
{
    private readonly IDealsUnitOfWork _unitOfWOrk = unitOfWOrk;

    public async Task<Result<Guid>> Handle(
        CreateProductBookCommand request, CancellationToken cancellationToken)
    {
        Result<ProductBook> result = ProductBook.Create(request.ProductBookId, request.Title);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        await _unitOfWOrk
            .GetWriteRepository<ProductBook>()
            .CreateAsync(result.Value, cancellationToken);

        await _unitOfWOrk.CommitAsync(cancellationToken);

        return Result.Success(result.Value.Id);
    }
}
