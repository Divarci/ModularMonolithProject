using Futions.CRM.Common.Infrastructure.MessageBox;

namespace Futions.CRM.Modules.Catalogue.Infrastructure.Outbox;
internal sealed class CatalogueOutboxOptions : IMessageBoxOptions
{
    public int IntervalInSeconds { get; set; }  

    public int BatchSize { get; set; }
}
