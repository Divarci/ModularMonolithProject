using Futions.CRM.Common.Domain.Entities;
using Futions.CRM.Modules.Deals.Domain.Deals;

namespace Futions.CRM.Modules.Deals.Domain.ShadowTables.Organisations;
public sealed partial class OrganisationPerson : BaseEntity, IAggregate
{
    private OrganisationPerson(Guid organisationId, Guid personId, string personFullname)
    {
        OrganisationId = organisationId;
        PersonId = personId;
        PersonFulname = personFullname;
    }

    public Guid OrganisationId { get; private set; }
    public Organisation Organisation { get; private set; }

    public Guid PersonId { get; private set; }
    public string PersonFulname { get; private set; }

    private readonly List<DealPerson> _dealPeople = [];
    public IReadOnlyCollection<DealPerson> DealPeople => _dealPeople.AsReadOnly();
}
