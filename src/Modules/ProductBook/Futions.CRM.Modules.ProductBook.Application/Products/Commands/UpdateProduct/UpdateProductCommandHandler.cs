using Futions.CRM.Common.Application.Messaging;
using Futions.CRM.Common.Domain.IUnitOfWorks;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Catalogue.Domain.Abstractions;
using Futions.CRM.Modules.Catalogue.Domain.ProductBooks;
using Futions.CRM.Modules.Catalogue.Domain.ProductBooks.Errors;
using Microsoft.EntityFrameworkCore;

namespace Futions.CRM.Modules.Catalogue.Application.Products.Commands.UpdateProduct;
internal sealed class UpdateProductCommandHandler(
    ICatalogueUnitOfWork unitOfWork) : ICommandHandler<UpdateProductCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(
        UpdateProductCommand request, CancellationToken cancellationToken)
    {
        ProductBook productBook = await _unitOfWork
            .GetReadRepository<ProductBook>()
            .Query(query => query
                .Include(x => x.Products)
                .SingleOrDefaultAsync(x => x.Id == request.ProductBookId, cancellationToken)
            );

        if (productBook is null)
        {
            return Result.Failure(ProductBookErrors.NotFound(request.ProductBookId));
        }

        if (string.IsNullOrEmpty(request.Title) && 
            string.IsNullOrEmpty(request.Description) && 
            !request.Price.HasValue)
        {
            return Result.Failure(Error.Conflict(
                "ProductBook.Conflict",
                "All fields can not be null or empty"));
        }

        if (!string.IsNullOrEmpty(request.Title))
        {
            Result result = productBook.UpdateProductTitle(request.ProductId, request.Title);

            if(result.IsFailure)
            {
                return Result.Failure(result.Error);
            }
        }

        if (!string.IsNullOrEmpty(request.Description))
        {
            Result result = productBook.UpdateProductDescription(request.ProductId, request.Description);

            if (result.IsFailure)
            {
                return Result.Failure(result.Error);
            }
        }

        if (request.Price.HasValue)
        {
            Result result = productBook.UpdateProductPrice(request.ProductId, request.Price.Value);

            if (result.IsFailure)
            {
                return Result.Failure(result.Error);
            }
        }

        _unitOfWork
           .GetWriteRepository<ProductBook>()
           .Update(productBook);

        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Success();
    }
}
