using Futions.CRM.Common.Domain.Entities;

namespace Futions.CRM.Modules.Users.Domain.Roles;
public sealed class Permission : IAggregate
{
    public static readonly Permission GetUser = new("users:read");
    public static readonly Permission ModifyUser = new("users:update");
    public static readonly Permission GetDeals = new("deals:read");
    public static readonly Permission ModifyDeals = new("deals:update");
    public static readonly Permission CreateDeals = new("deals:create");
    public static readonly Permission RemoveDeals = new("deals:remove");

    public Permission(string code)
    {
        Code = code;
    }

    public string Code { get; }
}
