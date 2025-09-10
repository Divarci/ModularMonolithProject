using Futions.CRM.Common.Application.EventBus;
using Futions.CRM.Common.Application.Messaging;
using Futions.CRM.Common.Domain.Exceptions;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Catalogue.Application.ProductBooks.Queries.GetProductBookById;
using Futions.CRM.Modules.Catalogue.Application.ProductBooks.Queries.Shared.Dto;
using Futions.CRM.Modules.Catalogue.Domain.ProductBooks.DomainEvents.ProductBookEvents;
using Futions.CRM.Modules.Catalogue.IntegrationEvents.ProductBook;
using MediatR;

namespace Futions.CRM.Modules.Catalogue.Application.ProductBooks.Commands.DeleteProductBook.EventHandlers;
internal sealed class ProductBookRemovedDomainEventHandler(
    ISender sender,
    IEventBus eventBus) : DomainEventHandler<ProductBookRemovedDomainEvent>
{
    public override async Task Handle(
        ProductBookRemovedDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        Result<ProductBookDto> productBookDtoResult = await sender.Send(
            new GetProductBookByIdQuery(domainEvent.ProductBookId), cancellationToken);

        if (productBookDtoResult.IsSuccess)
        {
            throw new CrmException(
                nameof(GetProductBookByIdQuery), Error.Problem(
                    "ProductBook.RemoveError",
                    "Product Book should have been deleted but not"));
        }

        await eventBus.PublishAsync(
            new ProductBookRemovedIntegrationEvent(
                domainEvent.Id,
                domainEvent.OccurredOnUtc,
                domainEvent.ProductBookId),
            cancellationToken);
    }
}
