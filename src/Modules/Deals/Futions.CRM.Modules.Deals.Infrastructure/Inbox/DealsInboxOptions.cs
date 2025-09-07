using Futions.CRM.Common.Infrastructure.Outbox;

namespace Futions.CRM.Modules.Deals.Infrastructure.Inbox;
internal sealed class DealsInboxOptions : IInboxOptions
{
    public int IntervalInSeconds { get; set; }  

    public int BatchSize { get; set; }
}
