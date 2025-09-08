using Futions.CRM.Common.Infrastructure.MessageBox;

namespace Futions.CRM.Modules.Deals.Infrastructure.Outbox;
internal sealed class DealsOutboxOptions : IMessageBoxOptions
{
    public int IntervalInSeconds { get; set; }  

    public int BatchSize { get; set; }
}
