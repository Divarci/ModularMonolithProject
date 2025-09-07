namespace Futions.CRM.Common.Infrastructure.Outbox;

public interface IInboxOptions
{
    int IntervalInSeconds { get; }

    int BatchSize { get; }
}
