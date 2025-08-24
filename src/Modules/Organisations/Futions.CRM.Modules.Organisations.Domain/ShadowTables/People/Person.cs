using Futions.CRM.Common.Domain.Entities;
using Futions.CRM.Common.Domain.ValueObjects;

namespace Futions.CRM.Modules.Organisations.Domain.ShadowTables.People;
public sealed partial class Person : BaseEntity, IRootAggregate
{
    private Person(string firstName, string? lastName)
    {
        Fullname = new Name(firstName, lastName);
    }

    public Name Fullname { get; private set; }
}
