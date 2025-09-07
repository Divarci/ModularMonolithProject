using Futions.CRM.Modules.Users.Domain.InboxMessages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Futions.CRM.Modules.Users.Infrastructure.Persistance.Configurations;
public class UsersInboxMessageConfig : IEntityTypeConfiguration<UsersInboxMessage>
{
    public void Configure(EntityTypeBuilder<UsersInboxMessage> builder)
    {
        builder.Property(o => o.Content)
            .HasColumnType("nvarchar(max)");
    }
}
