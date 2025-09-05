using Futions.CRM.Modules.Users.Domain.Permissions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Futions.CRM.Modules.Users.Infrastructure.Persistance.Configurations;
public class PermissionConfig : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.Property(x => x.Code)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasMany(x => x.RolePermissions)
            .WithOne(x => x.Permission)
            .HasForeignKey(ur => ur.PermissionId)
            .IsRequired()
            .OnDelete(DeleteBehavior.ClientCascade);

        //builder.HasData(
        //   Permission.GetDeals,
        //   Permission.CreateDeals,
        //   Permission.ModifyDeals,
        //   Permission.RemoveDeals,
        //   Permission.GetUser,
        //   Permission.ModifyUser);       
    }
}
