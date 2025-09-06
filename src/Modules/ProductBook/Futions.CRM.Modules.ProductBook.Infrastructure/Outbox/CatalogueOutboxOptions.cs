using Futions.CRM.Common.Infrastructure.Outbox;

namespace Futions.CRM.Modules.Catalogue.Infrastructure.Outbox;
internal sealed class CatalogueOutboxOptions : IOutboxOptions
{
    public int IntervalInSeconds { get; set; }  

    public int BatchSize { get; set; }
}
