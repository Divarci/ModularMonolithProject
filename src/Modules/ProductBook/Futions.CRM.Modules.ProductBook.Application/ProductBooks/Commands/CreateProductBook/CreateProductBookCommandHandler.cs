using Futions.CRM.Common.Application.Messaging;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Catalogue.Domain.Abstractions;
using Futions.CRM.Modules.Catalogue.Domain.ProductBooks;

namespace Futions.CRM.Modules.Catalogue.Application.ProductBooks.Commands.CreateProductBook;
internal sealed class CreateProductBookCommandHandler(
    ICatalogueUnitOfWork unitOfWOrk) : ICommandHandler<CreateProductBookCommand, ProductBook>
{
    private readonly ICatalogueUnitOfWork _unitOfWOrk = unitOfWOrk;

    public async Task<Result<ProductBook>> Handle(
        CreateProductBookCommand request, CancellationToken cancellationToken)
    {
        Result<ProductBook> result = ProductBook.Create(request.Title);

        if (result.IsFailure)
        {
            return Result.Failure<ProductBook>(result.Error);
        }

        await _unitOfWOrk
            .GetWriteRepository<ProductBook>()
            .CreateAsync(result.Value, cancellationToken);

        await _unitOfWOrk.CommitAsync(cancellationToken);

        return Result.Success(result.Value);
    }
}
