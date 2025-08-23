using Futions.CRM.Common.Domain.Entities;
using Futions.CRM.Modules.People.Domain.People.ValueObjects;

namespace Futions.CRM.Modules.People.Domain.ShadowTables.People;
public sealed partial class Person : BaseEntity, IRootAggregate
{
    private Person(string firstName, string? lastName)
    {
        Fullname = new Name(firstName, lastName);
    }

    public Name Fullname { get; private set; }
}
