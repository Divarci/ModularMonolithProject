using Futions.CRM.Common.Domain.Entities;

namespace Futions.CRM.Modules.Organisations.Domain.Organisations;
public sealed partial class OrganisationPerson : BaseEntity, IAggregate
{
    private OrganisationPerson(Guid organisationId, Guid personId)
    {
        OrganisationId = organisationId;
        PersonId = personId;
    }

    public Guid OrganisationId { get; private set; }
    public Organisation Organisation { get; private set; }

    public Guid PersonId { get; private set; }
    public Person Person { get; private set; }

    private readonly List<DealPerson> _dealPeople = [];
    public IReadOnlyCollection<DealPerson> DealPeople => _dealPeople.AsReadOnly();
}
