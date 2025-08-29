using Futions.CRM.Common.Application.Messaging;
using Futions.CRM.Common.Domain.IUnitOfWorks;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Deals.Domain.ShadowTables.Products;
using Futions.CRM.Modules.Deals.Domain.ShadowTables.Products.Errors;

namespace Futions.CRM.Modules.Deals.Application.Products.Commands.UpdateProduct;
internal sealed class UpdateProductCommandHandler(
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateProductCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(
        UpdateProductCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Title) &&
            string.IsNullOrEmpty(request.Description) &&
            !request.Price.HasValue)
        {
            return Result.Failure(Error.Conflict(
                "ProductBook.Conflict",
                "All fields can not be null or empty"));
        }

        Product product = await _unitOfWork
            .GetReadRepository<Product>()
            .GetByIdAsync(request.ProductId, cancellationToken);

        if (product is null)
        {
            return Result.Failure(ProductErrors.NotFound(request.ProductId));
        }

        if (!string.IsNullOrEmpty(request.Title))
        {
            Result result = product.UpdateTitle(request.Title);

            if (result.IsFailure)
            {
                return Result.Failure(result.Error);
            }
        }

        if (!string.IsNullOrEmpty(request.Description))
        {
            Result result = product.UpdateDescription(request.Description);

            if (result.IsFailure)
            {
                return Result.Failure(result.Error);
            }
        }

        if (request.Price.HasValue)
        {
            Result result = product.UpdatePrice(request.Price.Value);

            if (result.IsFailure)
            {
                return Result.Failure(result.Error);
            }
        }

        _unitOfWork
           .GetWriteRepository<Product>()
           .Update(product);

        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Success();
    }
}
