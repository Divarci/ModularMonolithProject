using Futions.CRM.Common.Application.EventBus;
using Futions.CRM.Common.Application.Messaging;
using Futions.CRM.Modules.Deals.Domain.ShadowTables.ProductBooks.DomainEvents;
using Futions.CRM.Modules.Deals.IntegrationEvents;

namespace Futions.CRM.Modules.Deals.Application.Products.Commands.DeleteProduct.Events;
internal sealed class ProductRemoveCompletedDomainEventHandler(
    IEventBus eventBus) : DomainEventHandler<ProductRemoveCompletedDomainEvent>
{
    private readonly IEventBus _eventBus = eventBus;

    public override async Task Handle(
        ProductRemoveCompletedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        await _eventBus.PublishAsync(new ProductRemoveCompletedIntegrationEvent(
            domainEvent.Id,
            domainEvent.ProductBookId,
            domainEvent.ProductId,
            domainEvent.OccurredOnUtc), cancellationToken);
    }
}
