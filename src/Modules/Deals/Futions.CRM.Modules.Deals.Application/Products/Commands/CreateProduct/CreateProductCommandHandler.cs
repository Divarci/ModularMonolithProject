using Futions.CRM.Common.Application.Messaging;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Deals.Domain.Abstractions;
using Futions.CRM.Modules.Deals.Domain.ShadowTables.Products;

namespace Futions.CRM.Modules.Deals.Application.Products.Commands.CreateProduct;
internal sealed class CreateProductCommandHandler(
    IDealsUnitOfWork unitOfWork) 
    : ICommandHandler<CreateProductCommand, Guid>
{
    private readonly IDealsUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<Guid>> Handle(
        CreateProductCommand request, CancellationToken cancellationToken)
    {
        Result<Product> result = Product.Create(
            productBookId: request.ProductBookId,
            title: request.Title,
            description: request.Description,
            price: request.Price);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        await _unitOfWork
            .GetWriteRepository<Product>()
            .CreateAsync(result.Value, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Success(result.Value.Id);
    }
}
