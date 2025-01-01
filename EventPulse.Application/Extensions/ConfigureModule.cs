using System.Reflection;
using EventPulse.Infrastructure.Extensions;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventPulse.Application.Extensions;

public static class ConfigureModule
{
    public static void ConfigureApplicationAndInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.ConfigureInfrastructure(GetConnectionString(configuration));
        services.ConfigureApplication();
    }

    private static void ConfigureApplication(this IServiceCollection services)
    {
        services.ConfigureValidators();
    }

    private static void ConfigureValidators(this IServiceCollection services)
        => services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

    private static string GetConnectionString(IConfiguration configuration)
    {
        return configuration.GetConnectionString("EventPulseConnection") ??
               Environment.GetEnvironmentVariable("DEFAULT_CONNECTION_STRING") ??
               throw new ArgumentNullException(nameof(configuration));
    }
}