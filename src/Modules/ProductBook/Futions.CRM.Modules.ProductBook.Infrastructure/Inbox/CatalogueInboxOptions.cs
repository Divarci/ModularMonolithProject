using Futions.CRM.Common.Infrastructure.Outbox;

namespace Futions.CRM.Modules.Catalogue.Infrastructure.Inbox;
internal sealed class CatalogueInboxOptions : IInboxOptions
{
    public int IntervalInSeconds { get; set; }  

    public int BatchSize { get; set; }
}
