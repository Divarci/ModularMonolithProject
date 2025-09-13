using Futions.CRM.Common.Application.EventBus;
using Futions.CRM.Common.Application.Messaging;
using Futions.CRM.Modules.Catalogue.Domain.ProductBooks.DomainEvents.ProductEvents;
using Futions.CRM.Modules.Catalogue.IntegrationEvents.Product;

namespace Futions.CRM.Modules.Catalogue.Application.Products.Commands.DeleteProduct.Events;
internal sealed class CheckProductIfCanbeRemovedDomainEventHandler(
    IEventBus eventBus) : DomainEventHandler<CheckProductIfCanbeRemovedDomainEvent>
{
    private readonly IEventBus _eventBus = eventBus;

    public override async Task Handle(
        CheckProductIfCanbeRemovedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        await _eventBus.PublishAsync(new CheckProductIfCanbeRemovedIntegrationEvent(
            domainEvent.Id,
            domainEvent.ProductBookId,
            domainEvent.ProductId,
            domainEvent.OccurredOnUtc), cancellationToken);
    }
}
