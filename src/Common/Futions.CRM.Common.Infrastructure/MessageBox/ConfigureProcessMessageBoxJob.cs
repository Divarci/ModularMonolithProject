using Microsoft.Extensions.Options;
using Quartz;

namespace Futions.CRM.Common.Infrastructure.MessageBox;
public class ConfigureProcessMessageBoxJob<TJob, TOptions>(IOptions<TOptions> inboxOptions)
    : IConfigureOptions<QuartzOptions>
    where TJob : IJob where TOptions : class, IMessageBoxOptions
{
    private readonly TOptions _inboxOptions = inboxOptions.Value;

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
                        schedule.WithIntervalInSeconds(_inboxOptions.IntervalInSeconds).RepeatForever())
            );
    }
}
