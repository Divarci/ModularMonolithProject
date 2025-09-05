using Futions.CRM.Common.Domain.Abstractions.AutoSeed;
using Futions.CRM.Common.Domain.Entities;
using Futions.CRM.Modules.Users.Domain.Permissions;

namespace Futions.CRM.Modules.Users.Domain.Roles;
public class RolePermission : BaseEntity, IAggregate, IHaveAutoseedData
{
    private RolePermission() { }

    private RolePermission(Guid id, Guid roleId, Guid permissionId)
    {
        Id = id;
        RoleId = roleId;
        PermissionId = permissionId;
    }
    public Guid RoleId { get; private set; }
    public Role Role { get; private set; }

    public Guid PermissionId { get; private set; }
    public Permission Permission { get; private set; }

    [AutoSeedData]
    public static readonly RolePermission MemberGetDeals = new(
        Guid.Parse("4AE14806-E433-43ED-A1CE-B80EBCD9A811"),
        Role.Member.Id, Permission.GetDeals.Id);

    [AutoSeedData]
    public static readonly RolePermission MemberCreateDeals = new(
        Guid.Parse("A2FCB4AF-B11B-48CB-925A-80BF668E0EBE"),
        Role.Member.Id, Permission.CreateDeals.Id);

    [AutoSeedData]
    public static readonly RolePermission MemberModifyDeals = new(
        Guid.Parse("07C74200-AD79-47CC-BFC5-6A03574D9EA1"),
        Role.Member.Id, Permission.ModifyDeals.Id);

    public static readonly RolePermission MemberRemoveDeals = new(
        Guid.Parse("F6C3F373-AFD8-41CF-A3D4-4D4B7A7A1AC2"),
        Role.Member.Id, Permission.RemoveDeals.Id);

    [AutoSeedData]
    public static readonly RolePermission MemberGetUser = new(
        Guid.Parse("5492E74B-97EC-42A2-B048-E08EB9C179C0"),
        Role.Member.Id, Permission.GetUser.Id);

    [AutoSeedData]
    public static readonly RolePermission AdministratorGetDeals = new(
        Guid.Parse("9613B245-C7F4-4D29-A740-81CD729A5158"),
        Role.Administrator.Id, Permission.GetDeals.Id);

    [AutoSeedData]
    public static readonly RolePermission AdministratorCreateDeals = new(
        Guid.Parse("28624AD4-D4F9-4DFD-81C9-9F8644BBF3CB"),
        Role.Administrator.Id, Permission.CreateDeals.Id);

    [AutoSeedData]
    public static readonly RolePermission AdministratorModifyDeals = new(
        Guid.Parse("BA67ED03-8A2A-48B4-BC9B-CF5440064CB3"),
        Role.Administrator.Id, Permission.ModifyDeals.Id);

    [AutoSeedData]
    public static readonly RolePermission AdministratorRemoveDeals = new(
        Guid.Parse("D9A55C23-3362-484F-BD5A-81337FFA59C2"),
        Role.Administrator.Id, Permission.RemoveDeals.Id);

    [AutoSeedData]
    public static readonly RolePermission AdministratorGetUser = new(
        Guid.Parse("4F5F60F4-530F-4CC0-9B61-19F39657F3B9"),
        Role.Administrator.Id, Permission.GetUser.Id);

    [AutoSeedData]
    public static readonly RolePermission AdministratorModifyUser = new(
        Guid.Parse("1EB17514-E8DA-48A0-B3E2-B14C204F1DF3"),
        Role.Administrator.Id, Permission.ModifyUser.Id);
}
