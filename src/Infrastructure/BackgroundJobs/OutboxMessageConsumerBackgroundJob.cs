using Domain.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Persistence;
using Persistence.Outbox;
using Quartz;
using SharedKernel;

namespace Infrastructure.BackgroundJobs;

[DisallowConcurrentExecution]
public class OutboxMessageConsumerBackgroundJob(
    ApplicationDbContext dbContext,
    IDateTimeProvider dateTimeProvider,
    ILogger<OutboxMessageConsumerBackgroundJob> logger,
    IPublisher publisher) : IJob
{
    private const int NumberOfAttempts = 5;

    private static readonly JsonSerializerSettings JsonSerializerSettings =
        new() { TypeNameHandling = TypeNameHandling.All };

    public async Task Execute(IJobExecutionContext context)
    {
        var messages = await dbContext.Set<OutboxMessage>()
            .Where(m => m.ProcessedAtUtc == null)
            .Take(10)
            .ToListAsync(context.CancellationToken);

        foreach (var message in messages)
        {
            var domainEvent = JsonConvert
                .DeserializeObject<IDomainEvent>(message.Content, JsonSerializerSettings);
            if (domainEvent is null)
            {
                logger.LogWarning("Investigate why this event was deserialized to null. OutboxMessage id: {MessageId}",
                    message.Id);
                continue;
            }

            for (var i = 0; i < NumberOfAttempts; i++)
            {
                await Task.Delay(i * 50, context.CancellationToken);
                try //This logic can be abstracted away using Polly 
                {
                    await publisher.Publish(domainEvent, context.CancellationToken);
                    message.MarkAsProcessed(dateTimeProvider.UtcNow);
                    break;
                }
                catch (Exception exception)
                {
                    message.MarkAsError(dateTimeProvider.UtcNow, exception.ToString());
                }
            }
        }

        await dbContext.SaveChangesAsync();
    }
}

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