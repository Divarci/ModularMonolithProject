using Futions.CRM.Common.Application.EventBus;
using Futions.CRM.Common.Application.Messaging;
using Futions.CRM.Common.Domain.Exceptions;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Catalogue.Application.ProductBooks.Queries.GetProductBookById;
using Futions.CRM.Modules.Catalogue.Application.ProductBooks.Queries.Shared.Dto;
using Futions.CRM.Modules.Catalogue.Domain.ProductBooks.DomainEvents.ProductBookEvents;
using Futions.CRM.Modules.Catalogue.IntegrationEvents.ProductBook;
using MediatR;

namespace Futions.CRM.Modules.Catalogue.Application.ProductBooks.Commands.UpdateProductBook.EventHandlers;
internal sealed class ProductBookStatusUpdateDomainEventHandler(
    ISender sender,
    IEventBus eventBus) : DomainEventHandler<ProductBookStatusUpdateDomainEvent>
{
    public override async Task Handle(
        ProductBookStatusUpdateDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        Result<ProductBookDto> productBookDtoResult = await sender.Send(
            new GetProductBookByIdQuery(domainEvent.ProductBookId), cancellationToken);

        if (productBookDtoResult.IsFailure)
        {
            throw new CrmException(
                nameof(GetProductBookByIdQuery), productBookDtoResult.Error);
        }

        await eventBus.PublishAsync(
            new ProductBookStatusUpdateIntegrationEvent(
                domainEvent.Id,
                domainEvent.OccurredOnUtc,
                productBookDtoResult.Value.Id,
                productBookDtoResult.Value.Inactive),
            cancellationToken);
    }
}
