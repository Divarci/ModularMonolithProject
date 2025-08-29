using Futions.CRM.Common.Application.Messaging;
using Futions.CRM.Common.Domain.IUnitOfWorks;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Catalogue.Domain.ProductBooks;
using Futions.CRM.Modules.Catalogue.Domain.ProductBooks.Errors;
using Microsoft.EntityFrameworkCore;

namespace Futions.CRM.Modules.Catalogue.Application.ProductBooks.Commands.DeleteProductBook;
internal sealed class DeleteProductBookCommandHandler(
    IUnitOfWork unitOfWork) : ICommandHandler<DeleteProductBookCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(
        DeleteProductBookCommand request, CancellationToken cancellationToken)
    {
        ProductBook productBook = await _unitOfWork
            .GetReadRepository<ProductBook>()
            .Query(query=>query
                .Include(x=>x.Products)
                .SingleOrDefaultAsync(x=>x.Id == request.ProductBookId, cancellationToken)
            );

        if (productBook is null)
        {
            return Result.Failure(ProductBookErrors.NotFound(request.ProductBookId));
        }

        if(productBook.Products.Count > 0)
        {
            return Result.Failure(ProductBookErrors.HasProducts);
        }

        _unitOfWork.GetWriteRepository<ProductBook>().Delete(productBook);

        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Success();
    }
}
