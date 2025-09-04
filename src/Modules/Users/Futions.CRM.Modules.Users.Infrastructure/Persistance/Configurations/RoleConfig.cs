using Futions.CRM.Modules.Users.Domain.Roles;
using Futions.CRM.Modules.Users.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Futions.CRM.Modules.Users.Infrastructure.Persistance.Configurations;
public class RoleConfig : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");

        builder.HasKey(x => x.Name);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(20);

        builder
            .HasMany<User>()
            .WithMany(x => x.Roles)
            .UsingEntity(joinBuilder =>
            {
                joinBuilder.ToTable("UserRoles");
                
                joinBuilder.Property("RolesName")
                    .HasColumnName("RoleName")
                    .HasMaxLength(20);
            });

        builder.HasData(Role.Administrator, Role.Member);
    }
}
