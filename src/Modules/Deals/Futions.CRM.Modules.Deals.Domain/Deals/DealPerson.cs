using Futions.CRM.Common.Domain.Entities;
using Futions.CRM.Modules.Deals.Domain.ShadowTables.Organisations;

namespace Futions.CRM.Modules.Deals.Domain.Deals;
public sealed partial class DealPerson : BaseEntity, IAggregate
{
    private DealPerson(Guid organisationPersonId, Guid dealId)
    {
        OrganisationPersonId = organisationPersonId;
        DealId = dealId;
    }

    public Guid OrganisationPersonId { get; private set; }
    public OrganisationPerson OrganisationPerson { get; private set; }

    public Guid DealId { get; private set; }
    public Deal Deal { get; private set; }
}
