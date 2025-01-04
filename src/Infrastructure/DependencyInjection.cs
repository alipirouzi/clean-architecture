using Infrastructure.BackgroundJobs;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using SharedKernel;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.ConfigureBackgroundJobs();
        return services;
    }

    private static void ConfigureBackgroundJobs(this IServiceCollection services)
    {
        services.AddQuartz();
        services.AddQuartzHostedService(options =>
        {
            options.WaitForJobsToComplete = true;
            options.StartDelay = TimeSpan.FromSeconds(5);
            options.AwaitApplicationStarted = true;
        });
        services.ConfigureOptions<OutboxMessageConsumerBackgroundJobSetup>();
    }
}