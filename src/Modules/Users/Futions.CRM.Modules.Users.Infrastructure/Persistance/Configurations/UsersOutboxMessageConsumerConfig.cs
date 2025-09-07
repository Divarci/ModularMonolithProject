using Futions.CRM.Modules.Users.Domain.OutboxMessages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Futions.CRM.Modules.Deals.Infrastructure.Persistance.Configurations;
public class UsersOutboxMessageConsumerConfig : IEntityTypeConfiguration<UsersOutboxMessageConsumer>
{
    public void Configure(EntityTypeBuilder<UsersOutboxMessageConsumer> builder)
    {
        builder.Property(o => o.Name)
            .IsRequired()
            .HasMaxLength(500);

        builder.HasKey(o => new { o.Id, o.Name });
    }
}
