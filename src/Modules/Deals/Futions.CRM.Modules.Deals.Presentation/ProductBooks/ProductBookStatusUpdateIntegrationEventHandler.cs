using Futions.CRM.Common.Application.EventBus;
using Futions.CRM.Common.Domain.Exceptions;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Catalogue.IntegrationEvents.ProductBook;
using Futions.CRM.Modules.Deals.Application.ProductBooks.Commands.UpdateProductBook;
using MediatR;

namespace Futions.CRM.Modules.Deals.Presentation.ProductBooks;
public sealed class ProductBookStatusUpdateIntegrationEventHandler(
    ISender sender) : IntegrationEventHandler<ProductBookStatusUpdateIntegrationEvent>
{
    public override async Task Handle(
        ProductBookStatusUpdateIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(
            new UpdateProductBookCommand(
                ProductBookId: integrationEvent.ProductBookId,
                Inactive: integrationEvent.Inactive),
                cancellationToken);

        if (result.IsFailure)
        {
            throw new CrmException(nameof(UpdateProductBookCommand), result.Error);
        }
    }
}
