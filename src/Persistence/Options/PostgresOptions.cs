using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Persistence.Options;

public sealed class PostgresOptions
{
    public const string ConfigSection = "PostgresOptions";

    public string ConnectionString { get; set; } = string.Empty;
    public int RetryCount { get; set; }
    public int CommandTimeout { get; set; }
    public bool EnableDetailedErrors { get; set; }
    public bool EnableSensitiveDataLogging { get; set; }
}

internal class PostgresOptionsSetup(IConfiguration configuration) : IConfigureOptions<PostgresOptions>
{
    public void Configure(PostgresOptions options)
    {
        configuration.GetSection(PostgresOptions.ConfigSection).Bind(options);
    }
}