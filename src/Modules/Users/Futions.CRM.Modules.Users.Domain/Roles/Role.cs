using Futions.CRM.Common.Domain.Entities;

namespace Futions.CRM.Modules.Users.Domain.Roles;
public sealed class Role : IAggregate
{
    public static readonly Role Administrator = new("Administrator");
    public static readonly Role Member = new("Member");

    private Role() { }

    private Role(string name)
    {
        Name = name;
    }

    public string Name { get; private set; }
}
