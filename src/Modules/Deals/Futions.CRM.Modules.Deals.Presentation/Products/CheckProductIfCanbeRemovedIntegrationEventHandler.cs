using Futions.CRM.Common.Application.EventBus;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Catalogue.IntegrationEvents.Product;
using Futions.CRM.Modules.Deals.Application.Products.Commands.DeleteProduct;
using Futions.CRM.Modules.Deals.IntegrationEvents;
using MediatR;

namespace Futions.CRM.Modules.Deals.Presentation.Products;
public sealed class CheckProductIfCanbeRemovedIntegrationEventHandler(
    ISender sender,
    IEventBus eventBus) : IntegrationEventHandler<CheckProductIfCanbeRemovedIntegrationEvent>
{
    private readonly ISender _sender = sender;

    public override async Task Handle(
        CheckProductIfCanbeRemovedIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await _sender.Send(
            new DeleteProductCommand(
                integrationEvent.ProductBookId,
                integrationEvent.ProductId),
                cancellationToken);

        if (result.IsFailure)
        {
            await eventBus.PublishAsync(
                new ProductRemoveFailedIntegrationEvent(
                    integrationEvent.Id,
                    integrationEvent.ProductBookId,
                    integrationEvent.ProductId,
                    integrationEvent.OccuredOnUtc),
                cancellationToken);
        }
    }
}
