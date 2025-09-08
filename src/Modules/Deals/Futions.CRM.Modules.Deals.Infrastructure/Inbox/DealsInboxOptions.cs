using Futions.CRM.Common.Infrastructure.MessageBox;

namespace Futions.CRM.Modules.Deals.Infrastructure.Inbox;
internal sealed class DealsInboxOptions : IMessageBoxOptions
{
    public int IntervalInSeconds { get; set; }  

    public int BatchSize { get; set; }
}
