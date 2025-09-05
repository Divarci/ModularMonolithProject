using Futions.CRM.Modules.Users.Domain.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Futions.CRM.Modules.Users.Infrastructure.Persistance.Configurations;
public class RoleConfig : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(20);

        builder.HasMany(x => x.UserRoles)
            .WithOne(x => x.Role)
            .HasForeignKey(ur => ur.RoleId)
            .IsRequired()
            .OnDelete(DeleteBehavior.ClientCascade);

        builder.HasMany(x => x.RolePermissions)
            .WithOne(x => x.Role)
            .HasForeignKey(ur => ur.RoleId)
            .IsRequired()
            .OnDelete(DeleteBehavior.ClientCascade);

        //builder.HasData(Role.Administrator, Role.Member);
    }
}
