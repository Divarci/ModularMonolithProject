using Futions.CRM.Modules.Users.Domain.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Futions.CRM.Modules.Users.Infrastructure.Persistance.Configurations;
public class RolePermissionConfig : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        //builder.HasData(
        //    RolePermission.MemberGetDeals,
        //    RolePermission.MemberCreateDeals,
        //    RolePermission.MemberModifyDeals,
        //    RolePermission.MemberRemoveDeals,
        //    RolePermission.MemberGetUser,
        //    RolePermission.AdministratorGetDeals,
        //    RolePermission.AdministratorCreateDeals,
        //    RolePermission.AdministratorModifyDeals,
        //    RolePermission.AdministratorRemoveDeals,
        //    RolePermission.AdministratorGetUser,
        //    RolePermission.AdministratorModifyUser);
    }
}


