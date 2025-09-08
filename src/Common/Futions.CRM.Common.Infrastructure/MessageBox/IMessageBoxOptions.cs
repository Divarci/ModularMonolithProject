namespace Futions.CRM.Common.Infrastructure.MessageBox;
public interface IMessageBoxOptions
{
    int IntervalInSeconds { get; }

    int BatchSize { get; }
}
