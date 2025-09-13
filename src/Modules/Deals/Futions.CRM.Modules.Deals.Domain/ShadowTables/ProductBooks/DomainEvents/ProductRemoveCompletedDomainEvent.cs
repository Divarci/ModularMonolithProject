using Futions.CRM.Common.Domain.DomainEvents;

namespace Futions.CRM.Modules.Deals.Domain.ShadowTables.ProductBooks.DomainEvents;
public sealed class ProductRemoveCompletedDomainEvent(
    Guid ProductBookId,
    Guid ProductId) : DomainEvent
{
    public Guid ProductBookId { get; set; } = ProductBookId;
    public Guid ProductId { get; set; } = ProductId;
}
