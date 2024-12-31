using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(AssemblyInfo.Assembly);
        });
        services.AddValidatorsFromAssembly(AssemblyInfo.Assembly);
        return services;
    }
}