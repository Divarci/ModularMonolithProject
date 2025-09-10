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

namespace Futions.CRM.Modules.Catalogue.Application.Products.Commands.DeleteProduct.Events;
internal sealed class ProductRemovedFromProductBookDomainEventHandler(
    ISender sender,
    IEventBus eventBus) : DomainEventHandler<ProductRemovedFromProductBookDomainEvent>
{
    public override async Task Handle(
        ProductRemovedFromProductBookDomainEvent domainEvent, 
        CancellationToken cancellationToken = default)
    {
        Result<ProductDto> productDto = await sender.Send(new GetProductByIdQuery(
            domainEvent.ProductBookId, domainEvent.ProductId), cancellationToken);

        if (productDto.IsSuccess)
        {
            throw new CrmException(
                nameof(GetProductBookByIdQuery), Error.Problem(
                    "Product.RemoveError",
                    "Product should have been deleted but not"));
        }

        await eventBus.PublishAsync(
            new ProductRemovedFromProductBookIntegrationEvent(
                domainEvent.Id,
                domainEvent.OccurredOnUtc,
                domainEvent.ProductBookId,
                domainEvent.ProductId),
            cancellationToken);
    }
}
