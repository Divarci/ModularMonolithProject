using Futions.CRM.Common.Application.Messaging;
using Futions.CRM.Common.Domain.IUnitOfWorks;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Catalogue.Domain.ProductBooks;
using Futions.CRM.Modules.Catalogue.Domain.ProductBooks.Errors;

namespace Futions.CRM.Modules.Catalogue.Application.ProductBooks.Commands.UpdateProductBook;
internal sealed class UpdateProductBookCommandHandler(
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateProductBookCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(
        UpdateProductBookCommand request, CancellationToken cancellationToken)
    {
        ProductBook productBook = await _unitOfWork
            .GetReadRepository<ProductBook>()
            .GetByIdAsync(request.ProductBookId, cancellationToken);

        if (productBook is null)
        {
            return Result.Failure(ProductBookErrors.NotFound(request.ProductBookId));
        }

        if (string.IsNullOrEmpty(request.Title) && !request.Inactive.HasValue)
        {
            return Result.Failure(Error.Problem(
                "ProductBook.Problem",
                "All fields can not be null or empty"));
        }

        if (!string.IsNullOrEmpty(request.Title))
        {
            Result result = productBook.UpdateTitle(request.Title);

            if (result.IsFailure)
            {
                return Result.Failure(result.Error);
            }
        }

        if (request.Inactive.HasValue)
        {
            Result result = request.Inactive.Value
                ? productBook.SetInactive()
                : productBook.SetActive();

            if (result.IsFailure)
            {
                return result;
            }
        }

        _unitOfWork
            .GetWriteRepository<ProductBook>()
            .Update(productBook);

        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Success();
    }
}
