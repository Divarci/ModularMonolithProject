using Futions.CRM.Common.Domain.Entities;

namespace Futions.CRM.Modules.Organisations.Domain.Organisations;
public sealed partial class Organisation : BaseEntity, IRootAggregate
{
    private Organisation(string title)
    {
        Title = title;
    }

    public string Title { get; private set; }


    private readonly List<OrganisationPerson> _organisationPeople = [];
    public IReadOnlyCollection<OrganisationPerson> OrganisationPeople => _organisationPeople.AsReadOnly();
}
