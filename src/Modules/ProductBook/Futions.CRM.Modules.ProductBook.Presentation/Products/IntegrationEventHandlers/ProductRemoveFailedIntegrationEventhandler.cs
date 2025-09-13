using Futions.CRM.Common.Application.EventBus;
using Futions.CRM.Common.Domain.Exceptions;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Catalogue.Application.Products.Commands.DeleteProduct;
using Futions.CRM.Modules.Deals.IntegrationEvents;
using MediatR;

namespace Futions.CRM.Modules.Catalogue.Presentation.Products.IntegrationEventHandlers;
public sealed class ProductRemoveFailedIntegrationEventhandler(
    ISender sender) : IntegrationEventHandler<ProductRemoveCompletedIntegrationEvent>
{
    private readonly ISender _sender = sender;

    public override async Task Handle(
        ProductRemoveCompletedIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await _sender.Send(
            new RevertProductCommand(
                integrationEvent.ProductBookId,
                integrationEvent.ProductId),
                cancellationToken);

        if (result.IsFailure)
        {
            throw new CrmException(nameof(RevertProductCommand), result.Error);
        }
    }
}

