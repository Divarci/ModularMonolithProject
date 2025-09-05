using Futions.CRM.Common.Domain.Abstractions.AutoSeed;
using Futions.CRM.Common.Domain.Entities;
using Futions.CRM.Modules.Users.Domain.Users;

namespace Futions.CRM.Modules.Users.Domain.Roles;
public class Role : BaseEntity, IRootAggregate
{
    private Role() { }

    private Role(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public string Name { get; private set; }

    private readonly List<UserRole> _userRoles = [];
    public IReadOnlyCollection<UserRole> UserRoles => _userRoles.AsReadOnly();

    private readonly List<RolePermission> _rolePermissions = [];
    public IReadOnlyCollection<RolePermission> RolePermissions => _rolePermissions.AsReadOnly();

    [AutoSeedData]
    public static readonly Role Administrator = new(
      Guid.Parse("4C2C722B-094B-4111-BDDB-0086AF5277ED"), "Administrator");

    [AutoSeedData]
    public static readonly Role Member = new(
        Guid.Parse("5DE1F66B-991D-49EA-AD1A-059B5EEBCF8D"), "Member");
}
