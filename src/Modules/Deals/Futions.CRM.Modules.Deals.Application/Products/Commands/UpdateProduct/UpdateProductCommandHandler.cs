using Futions.CRM.Common.Application.Messaging;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Deals.Domain.Abstractions;
using Futions.CRM.Modules.Deals.Domain.ShadowTables.ProductBooks;
using Futions.CRM.Modules.Deals.Domain.ShadowTables.ProductBooks.Errors;
using Microsoft.EntityFrameworkCore;

namespace Futions.CRM.Modules.Deals.Application.Products.Commands.UpdateProduct;
internal sealed class UpdateProductCommandHandler(
    IDealsUnitOfWork unitOfWork) : ICommandHandler<UpdateProductCommand>
{
    private readonly IDealsUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(
        UpdateProductCommand request, CancellationToken cancellationToken)
    {
        ProductBook productBook = await _unitOfWork
            .GetReadRepository<ProductBook>()
            .Query(query => query
                .AsTracking()
                .Include(x => x.Products)
                .SingleOrDefaultAsync(x => x.Id == request.ProductBookId, cancellationToken)
            );

        if (productBook is null)
        {
            return Result.Failure(ProductBookErrors.NotFound(request.ProductBookId));
        }
        
        Product product = productBook.Products
            .SingleOrDefault(x=>x.Id == request.ProductId);

        if (product is null)
        {
            return Result.Failure(ProductErrors.NotFound(request.ProductId));
        }

        if (product.Title != request.Title)
        {
            Result result = productBook.UpdateProductTitle(request.ProductId, request.Title!);

            if(result.IsFailure)
            {
                return Result.Failure(result.Error);
            }
        }

        if (product.Description != request.Description)
        {
            Result result = productBook.UpdateProductDescription(request.ProductId, request.Description);

            if (result.IsFailure)
            {
                return Result.Failure(result.Error);
            }
        }

        if (product.Price != request.Price)
        {
            Result result = productBook.UpdateProductPrice(request.ProductId, request.Price);

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
