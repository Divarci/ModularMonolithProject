using Futions.CRM.Common.Domain.Abstractions.AutoSeed;
using Futions.CRM.Common.Domain.Entities;
using Futions.CRM.Modules.Users.Domain.Roles;

namespace Futions.CRM.Modules.Users.Domain.Permissions;
public sealed class Permission : BaseEntity, IRootAggregate
{
    private Permission() { }

    private Permission(Guid id, string code)
    {
        Id = id;
        Code = code;
    }

    public string Code { get; }

    private readonly List<RolePermission> _rolePermissions = [];
    public IReadOnlyCollection<RolePermission> RolePermissions => _rolePermissions.AsReadOnly();

    [AutoSeedData]
    public static readonly Permission GetUser = new(
       Guid.Parse("C935B2BE-3D2F-44C4-9C2D-F00B5375AC48"), "users:read");
    
    [AutoSeedData]
    public static readonly Permission ModifyUser = new(
        Guid.Parse("818D85F9-5961-40A1-9D4A-E207E30BA783"), "users:update");

    [AutoSeedData]
    public static readonly Permission GetDeals = new(
        Guid.Parse("39675711-A006-4B01-BA29-0D933F268060"), "deals:read");

    [AutoSeedData]
    public static readonly Permission ModifyDeals = new(
        Guid.Parse("3ED06827-98B7-4916-BEF7-70E3119DD8EC"), "deals:update");

    [AutoSeedData]
    public static readonly Permission CreateDeals = new(
        Guid.Parse("E9338B23-2D61-4EC3-AA80-BA8FE59B65F0"), "deals:create");

    [AutoSeedData]
    public static readonly Permission RemoveDeals = new(
        Guid.Parse("46EFCA56-D955-4815-80F5-14D382471C52"), "deals:remove");

}
