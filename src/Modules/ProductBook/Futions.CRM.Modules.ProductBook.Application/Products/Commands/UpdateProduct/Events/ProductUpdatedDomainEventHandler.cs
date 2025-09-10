using Futions.CRM.Common.Application.EventBus;
using Futions.CRM.Common.Application.Messaging;
using Futions.CRM.Common.Domain.Exceptions;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Catalogue.Application.Products.Queries.GetAllProductsByProductBookId;
using Futions.CRM.Modules.Catalogue.Application.Products.Queries.GetProductById;
using Futions.CRM.Modules.Catalogue.Domain.ProductBooks.DomainEvents.ProductEvents;
using Futions.CRM.Modules.Catalogue.IntegrationEvents.Product;
using MediatR;

namespace Futions.CRM.Modules.Catalogue.Application.Products.Commands.UpdateProduct.Events;
internal sealed class ProductUpdatedDomainEventHandler(
    ISender sender,
    IEventBus eventBus) : DomainEventHandler<ProductUpdatedDomainEvent>
{
    public override async Task Handle(
        ProductUpdatedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        Result<ProductDto> productDtoResult = await sender.Send(new GetProductByIdQuery(
            domainEvent.ProductBookId, domainEvent.ProductId), cancellationToken);

        if (productDtoResult.IsFailure)
        {
            throw new CrmException(
                nameof(GetProductByIdQuery), productDtoResult.Error);
        }

        await eventBus.PublishAsync(
            new ProductUpdatedIntegrationEvent(
                domainEvent.Id,
                domainEvent.OccurredOnUtc,
                domainEvent.ProductBookId,
                domainEvent.ProductId,
                productDtoResult.Value.Title,
                productDtoResult.Value.Description,
                productDtoResult.Value.Price),
            cancellationToken);
    }
}
