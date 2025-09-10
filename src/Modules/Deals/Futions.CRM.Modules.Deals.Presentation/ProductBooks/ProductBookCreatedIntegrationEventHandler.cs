using Futions.CRM.Common.Application.EventBus;
using Futions.CRM.Common.Domain.Exceptions;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Catalogue.IntegrationEvents.ProductBook;
using Futions.CRM.Modules.Deals.Application.ProductBooks.Commands.CreateProductBook;
using MediatR;

namespace Futions.CRM.Modules.Deals.Presentation.ProductBooks;
public sealed class ProductBookCreatedIntegrationEventHandler(
    ISender sender) : IntegrationEventHandler<ProductBookCreatedIntegrationEvent>
{
    public override async Task Handle(
        ProductBookCreatedIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result<Guid> result = await sender.Send(
            new CreateProductBookCommand(
                integrationEvent.ProductBookId,
                integrationEvent.Title),
                cancellationToken);

        if (result.IsFailure)
        {
            throw new CrmException(nameof(CreateProductBookCommand), result.Error);
        }
    }
}
