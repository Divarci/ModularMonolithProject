using Futions.CRM.Common.Application.EventBus;
using Futions.CRM.Common.Domain.Exceptions;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Catalogue.IntegrationEvents.Product;
using Futions.CRM.Modules.Deals.Application.Products.Commands.CreateProduct;
using MediatR;

namespace Futions.CRM.Modules.Deals.Presentation.Products;
public sealed class ProductCreatedIntegrationEventHandler(
    ISender sender) : IntegrationEventHandler<ProductCreatedIntegrationEvent>
{
    public override async Task Handle(
        ProductCreatedIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result<Guid> result = await sender.Send(
            new CreateProductCommand(
                integrationEvent.ProductBookId,
                integrationEvent.ProductId,
                integrationEvent.Title,
                integrationEvent.Description,
                integrationEvent.Price),
                cancellationToken);

        if (result.IsFailure)
        {
            throw new CrmException(nameof(CreateProductCommand), result.Error);
        }
    }
}
