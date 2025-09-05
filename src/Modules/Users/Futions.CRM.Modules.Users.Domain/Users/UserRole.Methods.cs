namespace Futions.CRM.Modules.Users.Domain.Users;
public partial class UserRole
{
    public static UserRole Create(Guid userId, Guid roleId)
        => new(userId, roleId);
}
