using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Persistence.Options;
using SharedKernel;

namespace Persistence;

public class ApplicationDbContextFactory :
    IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddUserSecrets(AssemblyInfo.Assembly)
            .Build();

        var postgresOptions = new PostgresOptions()
        {
            ConnectionString = configuration["PostgresOptions:ConnectionString"]!,
            RetryCount = configuration.GetValue<int>("PostgresOptions:RetryCount"),
            CommandTimeout = configuration.GetValue<int>("PostgresOptions:CommandTimeout"),
            EnableDetailedErrors = configuration.GetValue<bool>("PostgresOptions:EnableDetailedErrors"),
            EnableSensitiveDataLogging = configuration.GetValue<bool>("PostgresOptions:EnableSensitiveDataLogging")
        };

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        Ensure.NotNullOrEmpty(postgresOptions.ConnectionString);

        optionsBuilder.UseNpgsql(postgresOptions.ConnectionString, postgresAction =>
        {
            postgresAction.EnableRetryOnFailure(postgresOptions.RetryCount);
            postgresAction.CommandTimeout(postgresOptions.CommandTimeout);
        });
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.EnableSensitiveDataLogging(postgresOptions.EnableSensitiveDataLogging);
        optionsBuilder.EnableDetailedErrors(postgresOptions.EnableDetailedErrors);

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}