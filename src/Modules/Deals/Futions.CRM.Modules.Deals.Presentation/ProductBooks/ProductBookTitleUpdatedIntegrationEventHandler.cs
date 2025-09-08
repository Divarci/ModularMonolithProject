using Futions.CRM.Common.Application.EventBus;
using Futions.CRM.Common.Domain.Exceptions;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Catalogue.IntegrationEvents;
using Futions.CRM.Modules.Deals.Application.ProductBooks.Commands.UpdateProductBook;
using MediatR;

namespace Futions.CRM.Modules.Deals.Presentation.ProductBooks;
public sealed class ProductBookTitleUpdatedIntegrationEventHandler(
    ISender sender) : IntegrationEventHandler<ProductBookTitleUpdatedIntegrationEvent>
{
    public override async Task Handle(
        ProductBookTitleUpdatedIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(
            new UpdateProductBookCommand(
                integrationEvent.ProductBookId,
                integrationEvent.Title),
                cancellationToken);

        if (result.IsFailure)
        {
            throw new CrmException(nameof(UpdateProductBookCommand), result.Error);
        }
    }
}
