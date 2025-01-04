using Microsoft.Extensions.Options;
using Quartz;

namespace Infrastructure.BackgroundJobs;

internal sealed class OutboxMessageConsumerBackgroundJobSetup : IConfigureOptions<QuartzOptions>
{
    public void Configure(QuartzOptions options)
    {
        var jobKey = JobKey.Create(nameof(OutboxMessageConsumerBackgroundJob));
        options.AddJob<OutboxMessageConsumerBackgroundJob>(jobBuilder => jobBuilder
                .WithIdentity(jobKey))
            .AddTrigger(trigger => trigger.ForJob(jobKey)
                .WithSimpleSchedule(schedule => schedule
                    .WithIntervalInSeconds(1)
                    .RepeatForever()));
    }
}