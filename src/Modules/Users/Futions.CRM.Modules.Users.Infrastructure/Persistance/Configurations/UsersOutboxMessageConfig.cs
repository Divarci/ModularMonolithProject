using Futions.CRM.Modules.Users.Domain.OutboxMessages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Futions.CRM.Modules.Users.Infrastructure.Persistance.Configurations;
public class UsersOutboxMessageConfig : IEntityTypeConfiguration<UsersOutboxMessage>
{
    public void Configure(EntityTypeBuilder<UsersOutboxMessage> builder)
    {
        builder.Property(o => o.Content)
            .HasColumnType("nvarchar(max)");
    }
}
