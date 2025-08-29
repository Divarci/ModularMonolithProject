using Futions.CRM.Common.Application.Messaging;
using Futions.CRM.Common.Domain.IUnitOfWorks;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Deals.Domain.ShadowTables.Products;

namespace Futions.CRM.Modules.Deals.Application.Products.Commands.CreateProduct;
internal sealed class CreateProductCommandHandler(
    IUnitOfWork unitOfWork) : ICommandHandler<CreateProductCommand, Product>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<Product>> Handle(
        CreateProductCommand request, CancellationToken cancellationToken)
    {
        Result<Product> result = Product.Create(
            productBookId: request.ProductBookId,
            title: request.Title,
            description: request.Description,
            price: request.Price);

        if (result.IsFailure)
        {
            return Result.Failure<Product>(result.Error);
        }

        await _unitOfWork
            .GetWriteRepository<Product>()
            .CreateAsync(result.Value, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Success(result.Value);
    }
}
