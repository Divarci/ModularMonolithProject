using Futions.CRM.Modules.Users.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Futions.CRM.Modules.Users.Infrastructure.Persistance.Configurations;
public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.Firstname)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(x => x.Lastname)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(x => x.IdentityId)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(64);

        builder.HasIndex(x => x.Email)
            .IsUnique();

        builder.HasIndex(x => x.IdentityId)
            .IsUnique();
    }
}
