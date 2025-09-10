using Futions.CRM.Common.Application.Messaging;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Deals.Domain.Abstractions;
using Futions.CRM.Modules.Deals.Domain.ShadowTables.ProductBooks;
using Futions.CRM.Modules.Deals.Domain.ShadowTables.ProductBooks.Errors;
using Microsoft.EntityFrameworkCore;

namespace Futions.CRM.Modules.Deals.Application.Products.Commands.DeleteProduct;
internal sealed class DeleteProductCommandHandler(
    IDealsUnitOfWork unitOfWork) : ICommandHandler<DeleteProductCommand>
{
    private readonly IDealsUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(
        DeleteProductCommand request, CancellationToken cancellationToken)
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

        Result result = productBook.RemoveProduct(request.ProductId);

        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        _unitOfWork.GetWriteRepository<ProductBook>().Update(productBook);

        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Success();
    }
}
