using Futions.CRM.Common.Application.Messaging;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Catalogue.Domain.Abstractions;
using Futions.CRM.Modules.Catalogue.Domain.ProductBooks;
using Futions.CRM.Modules.Catalogue.Domain.ProductBooks.Errors;
using Futions.CRM.Modules.Catalogue.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace Futions.CRM.Modules.Catalogue.Application.Products.Commands.CreateProduct;
internal sealed class CreateProductCommandHandler(
    ICatalogueUnitOfWork unitOfWork) : ICommandHandler<CreateProductCommand, Guid>
{
    private readonly ICatalogueUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<Guid>> Handle(
        CreateProductCommand request, CancellationToken cancellationToken)
    {
        ProductBook productBook = await _unitOfWork
            .GetReadRepository<ProductBook>()
            .Query(query => query
                .Include(x => x.Products)
                .SingleOrDefaultAsync(x => x.Id == request.ProductBookId, cancellationToken)
            );

        if (productBook is null)
        {
            return Result.Failure<Guid>(ProductBookErrors.NotFound(request.ProductBookId));
        }

        Result<Product> result = Product.Create(
            productBookId: request.ProductBookId,
            title: request.Title,
            description: request.Description,
            price: request.Price);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        productBook.AddProduct(result.Value);

        _unitOfWork
            .GetWriteRepository<ProductBook>()
            .Update(productBook);
        
        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Success(result.Value.Id);
    }
}
