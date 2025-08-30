using Futions.CRM.Common.Application.Messaging;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Catalogue.Application.ProductBooks.Queries.Shared.Dto;
using Futions.CRM.Modules.Catalogue.Domain.Abstractions;
using Futions.CRM.Modules.Catalogue.Domain.ProductBooks;
using Futions.CRM.Modules.Catalogue.Domain.ProductBooks.Errors;

namespace Futions.CRM.Modules.Catalogue.Application.ProductBooks.Queries.GetProductBookById;
internal sealed class GetProductBookByIdQueryHandler(
    ICatalogueUnitOfWork unitOfWork) : IQueryHandler<GetProductBookByIdQuery, ProductBookDto>
{
    private readonly ICatalogueUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<ProductBookDto>> Handle(
        GetProductBookByIdQuery request, CancellationToken cancellationToken)
    {
        ProductBook productBook = await _unitOfWork
            .GetReadRepository<ProductBook>()
            .GetByIdAsync(request.ProductBookId, cancellationToken);

        if(productBook is null)
        {
            return Result.Failure<ProductBookDto>(
                ProductBookErrors.NotFound(request.ProductBookId));
        }

        return Result.Success(new ProductBookDto
        {
            Id = productBook.Id,
            Inactive = productBook.Inactive,
            Title = productBook.Title
        });
    }
}
