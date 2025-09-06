using Futions.CRM.Common.Infrastructure.Outbox;

namespace Futions.CRM.Modules.Users.Infrastructure.Outbox;
internal sealed class UsersOutboxOptions : IOutboxOptions
{
    public int IntervalInSeconds { get; set; }  

    public int BatchSize { get; set; }
}
