using Futions.CRM.Common.Infrastructure.MessageBox;

namespace Futions.CRM.Modules.Users.Infrastructure.Outbox;
internal sealed class UsersOutboxOptions : IMessageBoxOptions
{
    public int IntervalInSeconds { get; set; }  

    public int BatchSize { get; set; }
}
