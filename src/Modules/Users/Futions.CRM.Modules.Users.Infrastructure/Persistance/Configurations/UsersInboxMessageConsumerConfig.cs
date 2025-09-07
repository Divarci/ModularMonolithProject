using Futions.CRM.Modules.Users.Domain.InboxMessages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Futions.CRM.Modules.Users.Infrastructure.Persistance.Configurations;
public class UsersInboxMessageConsumerConfig : IEntityTypeConfiguration<UsersInboxMessageConsumer>
{
    public void Configure(EntityTypeBuilder<UsersInboxMessageConsumer> builder)
    {
        builder.Property(o => o.Name)
            .IsRequired()
            .HasMaxLength(500);

        builder.HasKey(o => new { o.Id, o.Name });
    }
}
