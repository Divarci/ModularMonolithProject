using Futions.CRM.Common.Application.EventBus;
using Futions.CRM.Common.Application.Messaging;
using Futions.CRM.Common.Domain.Exceptions;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Catalogue.Application.ProductBooks.Queries.GetProductBookById;
using Futions.CRM.Modules.Catalogue.Application.Products.Queries.GetAllProductsByProductBookId;
using Futions.CRM.Modules.Catalogue.Application.Products.Queries.GetProductById;
using Futions.CRM.Modules.Catalogue.Domain.ProductBooks.DomainEvents.ProductEvents;
using Futions.CRM.Modules.Catalogue.IntegrationEvents.Product;
using MediatR;

namespace Futions.CRM.Modules.Catalogue.Application.Products.Commands.CreateProduct.Events;
internal sealed class ProductCreatedDomainEventHandler(
    ISender sender,
    IEventBus eventBus) : DomainEventHandler<ProductCreatedDomainEvent>
{
    public override async Task Handle(
        ProductCreatedDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        Result<ProductDto> productDto = await sender.Send(new GetProductByIdQuery(
            domainEvent.ProductBookId, domainEvent.ProductId), cancellationToken);

        if (productDto.IsFailure)
        {
            throw new CrmException(
                nameof(GetProductBookByIdQuery), productDto.Error);
        }

        await eventBus.PublishAsync(
            new ProductCreatedIntegrationEvent(
                domainEvent.Id,
                domainEvent.OccurredOnUtc,
                domainEvent.ProductBookId,
                domainEvent.ProductId,
                productDto.Value.Description,
                productDto.Value.Title,
                productDto.Value.Price),
            cancellationToken);
    }
}
