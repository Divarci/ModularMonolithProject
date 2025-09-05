using Futions.CRM.Common.Domain.Entities;
using Futions.CRM.Modules.Users.Domain.Roles;

namespace Futions.CRM.Modules.Users.Domain.Users;
public partial class UserRole : BaseEntity, IAggregate
{
    private UserRole() { }

    private UserRole(Guid userId, Guid roleId)
    {
        UserId = userId;
        RoleId = roleId;
    }

    public Guid UserId { get; private set; }
    public User User { get; private set; }

    public Guid RoleId { get; private set; }
    public Role Role { get; private set; }
}
