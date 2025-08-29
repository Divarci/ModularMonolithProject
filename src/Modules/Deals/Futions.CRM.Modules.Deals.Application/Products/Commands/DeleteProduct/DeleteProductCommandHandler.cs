using Futions.CRM.Common.Application.Messaging;
using Futions.CRM.Common.Domain.IUnitOfWorks;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Deals.Domain.ShadowTables.Products;
using Futions.CRM.Modules.Deals.Domain.ShadowTables.Products.Errors;
using Microsoft.EntityFrameworkCore;

namespace Futions.CRM.Modules.Deals.Application.Products.Commands.DeleteProduct;
internal sealed class DeleteProductCommandHandler(
    IUnitOfWork unitOfWork) : ICommandHandler<DeleteProductCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(
        DeleteProductCommand request, CancellationToken cancellationToken)
    {
        Product product = await _unitOfWork
            .GetReadRepository<Product>()
            .QueryAsync(q => q
                .Include(x => x.DealProducts)
                .SingleOrDefaultAsync(x=>x.Id == request.ProductId, cancellationToken));

        if (product is null)
        {
            return Result.Failure(ProductErrors.NotFound(request.ProductId));
        }

        if(product.DealProducts.Count > 0)
        {
            return Result.Failure(ProductErrors.HasDealProducts);
        }

        _unitOfWork
            .GetWriteRepository<Product>()
            .Delete(product);

        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Success();
    }
}
