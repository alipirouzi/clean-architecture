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
        //once a background job is added, it runs when the application starts.
        //to change this behaviour, add an option to the job you don't want to run at startup.
        
        services.AddQuartz();
        services.ConfigureOptions<OutboxMessageConsumerBackgroundJobSetup>();
    }
}