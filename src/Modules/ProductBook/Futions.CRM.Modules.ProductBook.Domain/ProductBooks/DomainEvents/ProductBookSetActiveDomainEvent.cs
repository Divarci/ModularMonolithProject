using Futions.CRM.Common.Domain.DomainEvents;

namespace Futions.CRM.Modules.Catalogue.Domain.ProductBooks.DomainEvents;
public sealed class ProductBookSetActiveDomainEvent(Guid productBookId) : DomainEvent
{
    public Guid ProductBookId { get; set; } = productBookId;
}
