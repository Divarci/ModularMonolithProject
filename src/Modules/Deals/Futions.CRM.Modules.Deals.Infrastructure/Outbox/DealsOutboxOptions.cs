using Futions.CRM.Common.Infrastructure.Outbox;

namespace Futions.CRM.Modules.Deals.Infrastructure.Outbox;
internal sealed class DealsOutboxOptions : IOutboxOptions
{
    public int IntervalInSeconds { get; set; }  

    public int BatchSize { get; set; }
}
