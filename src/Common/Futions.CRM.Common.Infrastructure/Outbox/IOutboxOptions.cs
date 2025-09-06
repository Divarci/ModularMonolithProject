namespace Futions.CRM.Common.Infrastructure.Outbox;

public interface IOutboxOptions
{
    int IntervalInSeconds { get; }

    int BatchSize { get; }
}
