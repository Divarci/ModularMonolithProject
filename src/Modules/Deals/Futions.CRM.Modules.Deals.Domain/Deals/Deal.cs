using Futions.CRM.Common.Domain.Entities;

namespace Futions.CRM.Modules.Deals.Domain.Deals;
public partial class Deal : BaseEntity, IRootAggregate
{
    private Deal(string title)
    {
        Id = Guid.NewGuid();
        Title = title;
        DealStatus = DealStatus.Open;
    }
    public string Title { get; private set; }
    public DealStatus DealStatus { get; private set; }


    private readonly List<DealProduct> _dealProducts = [];
    public IReadOnlyCollection<DealProduct> DealProducts => _dealProducts.AsReadOnly();
}
