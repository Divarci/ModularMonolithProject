using Futions.CRM.Modules.Users.Domain.Roles;
using Futions.CRM.Modules.Users.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Futions.CRM.Modules.Users.Infrastructure.Persistance.Configurations;
public class PermissionConfig : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("Permissions");

        builder.HasKey(x => x.Code);

        builder.Property(x => x.Code)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasData(
           Permission.GetDeals,
           Permission.CreateDeals,
           Permission.ModifyDeals,
           Permission.RemoveDeals,
           Permission.GetUser,
           Permission.ModifyUser);

        builder
            .HasMany<Role>()
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "RolePermissions",
                right => right.HasOne<Role>().WithMany().HasForeignKey("RolesName"),
                left => left.HasOne<Permission>().WithMany().HasForeignKey("PermissionCode"),
                joinBuilder =>
                {
                    joinBuilder.ToTable("RolePermissions");

                    joinBuilder.HasKey("RolesName", "PermissionCode");

                    joinBuilder.HasData(
                        CreateRolePermission(Role.Member, Permission.GetDeals),
                        CreateRolePermission(Role.Member, Permission.CreateDeals),
                        CreateRolePermission(Role.Member, Permission.ModifyDeals),
                        CreateRolePermission(Role.Member, Permission.RemoveDeals),
                        CreateRolePermission(Role.Member, Permission.GetUser),
                        CreateRolePermission(Role.Administrator, Permission.GetDeals),
                        CreateRolePermission(Role.Administrator, Permission.CreateDeals),
                        CreateRolePermission(Role.Administrator, Permission.ModifyDeals),
                        CreateRolePermission(Role.Administrator, Permission.RemoveDeals),
                        CreateRolePermission(Role.Administrator, Permission.GetUser),
                        CreateRolePermission(Role.Administrator, Permission.ModifyUser));
                });
    }

    private static object CreateRolePermission(Role role, Permission permission)
        => new
        {
            RolesName = role.Name,
            PermissionCode = permission.Code
        };
}
