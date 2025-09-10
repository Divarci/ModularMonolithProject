using Futions.CRM.Common.Application.EventBus;
using Futions.CRM.Common.Domain.Exceptions;
using Futions.CRM.Modules.Catalogue.IntegrationEvents.Product;
using Futions.CRM.Modules.Deals.Application.Products.Commands.UpdateProduct;
using MediatR;
using Result = Futions.CRM.Common.Domain.Results.Result;

namespace Futions.CRM.Modules.Deals.Presentation.Products;
public sealed class ProductUpdatedIntegrationEventHandler(
    ISender sender) : IntegrationEventHandler<ProductUpdatedIntegrationEvent>
{
    public override async Task Handle(
        ProductUpdatedIntegrationEvent integrationEvent, 
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(
            new UpdateProductCommand(
                integrationEvent.ProductBookId,
                integrationEvent.ProductId, 
                integrationEvent.Title,
                integrationEvent.Description,
                integrationEvent.Price),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new CrmException(nameof(UpdateProductCommand), result.Error);
        }
    }
}
