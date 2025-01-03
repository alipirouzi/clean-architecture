using Application.Abstractions;
using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Persistence.Interceptors;
using Persistence.Options;
using Persistence.Repositories;
using SharedKernel;

namespace Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.ConfigureOptions<PostgresOptionsSetup>();

        services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();

        services.AddDbContext<ApplicationDbContext>((serviceProvider, dbContextOptionsBuilder) =>
        {
            var dbOptions = serviceProvider.GetService<IOptions<PostgresOptions>>()!.Value;
            Ensure.NotNullOrEmpty(dbOptions.ConnectionString);
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

            dbContextOptionsBuilder.UseNpgsql(dbOptions.ConnectionString, postgresAction =>
            {
                postgresAction.EnableRetryOnFailure(dbOptions.RetryCount);
                postgresAction.CommandTimeout(dbOptions.CommandTimeout);
            });
            dbContextOptionsBuilder.UseSnakeCaseNamingConvention();
            dbContextOptionsBuilder.EnableDetailedErrors(dbOptions.EnableDetailedErrors);
            dbContextOptionsBuilder.EnableSensitiveDataLogging(dbOptions.EnableSensitiveDataLogging);
            if (dbOptions.EnableSensitiveDataLogging)
                dbContextOptionsBuilder
                    .AddInterceptors(
                        serviceProvider.GetRequiredService<ConvertDomainEventsToOutboxMessagesInterceptor>())
                    .UseLoggerFactory(loggerFactory).LogTo(Console.WriteLine, LogLevel.Information);
            else
                dbContextOptionsBuilder.AddInterceptors(serviceProvider
                    .GetRequiredService<ConvertDomainEventsToOutboxMessagesInterceptor>());
        });
        services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());
        services.AddTransient<IOrderRepository, OrderRepository>();

        return services;
    }
}