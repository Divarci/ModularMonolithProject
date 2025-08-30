using Futions.CRM.Common.Application.Messaging;
using Futions.CRM.Common.Domain.IUnitOfWorks;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Catalogue.Application.ProductBooks.Queries.Shared.Dto;
using Futions.CRM.Modules.Catalogue.Domain.Abstractions;
using Futions.CRM.Modules.Catalogue.Domain.ProductBooks;
using Microsoft.EntityFrameworkCore;

namespace Futions.CRM.Modules.Catalogue.Application.ProductBooks.Queries.GetAllProductBooks;
internal sealed class GetAllProductBooksQueryHandler(
    ICatalogueUnitOfWork unitOfWork) : IQueryHandler<GetAllProductBooksQuery, ProductBookDto[]>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<ProductBookDto[]>> Handle(
        GetAllProductBooksQuery request, CancellationToken cancellationToken)
    {
        ProductBookDto[] productBooks = await _unitOfWork.GetReadRepository<ProductBook>()
            .QueryAsync(query => query
                .Select(x => new ProductBookDto()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Inactive = x.Inactive
                })
                .ToArrayAsync(cancellationToken)
             );

        return Result.Success(productBooks);
    }
}
