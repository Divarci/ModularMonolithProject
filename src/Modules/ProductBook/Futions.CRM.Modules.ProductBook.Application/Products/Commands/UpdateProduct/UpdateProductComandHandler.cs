using Futions.CRM.Common.Application.Messaging;
using Futions.CRM.Common.Domain.IUnitOfWorks;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Catalogue.Domain.ProductBooks;
using Microsoft.EntityFrameworkCore;

namespace Futions.CRM.Modules.Catalogue.Application.Products.Commands.UpdateProduct;
internal sealed class UpdateProductComandHandler(
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateProductComand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(
        UpdateProductComand request, CancellationToken cancellationToken)
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

        if (!string.IsNullOrEmpty(request.Title) && 
            !string.IsNullOrEmpty(request.Description) && 
            !request.Price.HasValue)
        {
            return Result.Failure(Error.Conflict(
                "ProductBook.Conflict",
                "All fields can not be null or empty"));
        }

        if (!string.IsNullOrEmpty(request.Title))
        {
            productBook.UpdateProductTitle(request.ProductId, request.Title);
        }

        if (!string.IsNullOrEmpty(request.Description))
        {
            productBook.UpdateProductDescription(request.ProductId, request.Description);
        }

        if (request.Price.HasValue)
        {
            productBook.UpdateProductPrice(request.ProductId, request.Price.Value);
        }

        _unitOfWork
           .GetWriteRepository<ProductBook>()
           .Update(productBook);

        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Success();
    }
}
