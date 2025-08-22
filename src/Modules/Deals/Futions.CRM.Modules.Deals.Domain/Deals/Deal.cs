using Futions.CRM.Common.Domain.Entities;
using Futions.CRM.Modules.Deals.Domain.ShadowTables.Organisations;

namespace Futions.CRM.Modules.Deals.Domain.Deals;
public partial class Deal : BaseEntity, IRootAggregate
{
    private Deal(string title, Guid organisationId, Guid organisationPersonId)
    {
        Id = Guid.NewGuid();
        Title = title;
        OrganisationId = organisationId;
        PrimaryDealPersonId = organisationPersonId;
    }
    public string Title { get; private set; }

    public Guid PrimaryDealPersonId { get; private set; }
    public DealPerson PrimaryPerson { get; private set; }

    public Guid OrganisationId { get; private set; }
    public Organisation Organisation { get; private set; }


    private readonly List<DealPerson> _dealPeople = [];
    public IReadOnlyCollection<DealPerson> DealPeople => _dealPeople.AsReadOnly();


    private readonly List<DealProduct> _dealProducts = [];
    public IReadOnlyCollection<DealProduct> DealProducts => _dealProducts.AsReadOnly();
}
