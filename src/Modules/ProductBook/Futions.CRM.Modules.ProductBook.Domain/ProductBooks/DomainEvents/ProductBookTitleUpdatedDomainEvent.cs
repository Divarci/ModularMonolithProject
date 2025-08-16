using Futions.CRM.Common.Domain.DomainEvents;

namespace Futions.CRM.Modules.Catalogue.Domain.ProductBooks.DomainEvents;
public sealed class ProductBookTitleUpdatedDomainEvent(
    Guid productBookId,
    string title) : DomainEvent
{
    public Guid ProductBookId { get; set; } = productBookId;
    public string Title { get; set; } = title;
}
