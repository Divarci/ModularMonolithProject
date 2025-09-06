using Microsoft.Extensions.Options;
using Quartz;

namespace Futions.CRM.Common.Infrastructure.Outbox;
public sealed class ConfigureProcessOutboxJob<TJob, TOptions>(IOptions<TOptions> outboxOptions)
    : IConfigureOptions<QuartzOptions>
    where TJob : IJob where TOptions : class, IOutboxOptions
{
    private readonly TOptions _outboxOptions = outboxOptions.Value;

    public void Configure(QuartzOptions options)
    {
        string jobName = typeof(TJob).FullName!;

        options
            .AddJob<TJob>(configure => 
                configure
                    .WithIdentity(jobName)
                    .StoreDurably()
            )
            .AddTrigger(configure =>
                configure
                    .ForJob(jobName)
                    .WithSimpleSchedule(schedule =>
                        schedule.WithIntervalInSeconds(_outboxOptions.IntervalInSeconds).RepeatForever())
            );
    }
}
