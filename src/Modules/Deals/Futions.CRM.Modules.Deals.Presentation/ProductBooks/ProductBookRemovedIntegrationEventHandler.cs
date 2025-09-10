using Futions.CRM.Common.Application.EventBus;
using Futions.CRM.Common.Domain.Exceptions;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Catalogue.IntegrationEvents.ProductBook;
using Futions.CRM.Modules.Deals.Application.ProductBooks.Commands.DeleteProductBook;
using MediatR;

namespace Futions.CRM.Modules.Deals.Presentation.ProductBooks;
public sealed class ProductBookRemovedIntegrationEventHandler(
    ISender sender) : IntegrationEventHandler<ProductBookRemovedIntegrationEvent>
{
    public override async Task Handle(
        ProductBookRemovedIntegrationEvent integrationEvent, 
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(new DeleteProductBookCommand(
            integrationEvent.ProductBookId), cancellationToken);

        if (result.IsFailure)
        {
            throw new CrmException(nameof(DeleteProductBookCommand), result.Error);
        }
    }
}
