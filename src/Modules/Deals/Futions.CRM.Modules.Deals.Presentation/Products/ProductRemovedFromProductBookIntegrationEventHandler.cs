using Futions.CRM.Common.Application.EventBus;
using Futions.CRM.Common.Domain.Exceptions;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Catalogue.IntegrationEvents.Product;
using Futions.CRM.Modules.Deals.Application.Products.Commands.DeleteProduct;
using MediatR;

namespace Futions.CRM.Modules.Deals.Presentation.Products;
public sealed class ProductRemovedFromProductBookIntegrationEventHandler(
    ISender sender) : IntegrationEventHandler<ProductRemovedFromProductBookIntegrationEvent>
{
    public override async Task Handle(
        ProductRemovedFromProductBookIntegrationEvent integrationEvent, 
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(new DeleteProductCommand(
            integrationEvent.ProductBookId,
            integrationEvent.ProductId),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new CrmException(nameof(DeleteProductCommand), result.Error);
        }
    }
}
