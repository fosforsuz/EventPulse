using System.Reflection;
using EventPulse.Application.Modals;
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
        var (secret, issuer, audience) = GetSecretAndIssuer(configuration);
        services.ConfigureInfrastructure(GetConnectionString(configuration), secret,
            issuer, audience);
        services.ConfigureApplication(configuration: configuration);
    }

    private static void ConfigureApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureFileStoragePath(configuration);
        services.ConfigureValidators();
    }

    private static void ConfigureValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }

    private static void ConfigureFileStoragePath(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<FileStorageSettings>(configuration.GetSection("FileStorageSettings"));
    }

    private static string GetConnectionString(IConfiguration configuration)
    {
        return configuration.GetConnectionString("EventPulseConnection") ??
               Environment.GetEnvironmentVariable("DEFAULT_CONNECTION_STRING") ??
               throw new ArgumentNullException(nameof(configuration));
    }

    private static (string, string, string) GetSecretAndIssuer(IConfiguration configuration)
    {
        var secret = configuration["Jwt:Secret"] ??
                     Environment.GetEnvironmentVariable("JWT_SECRET") ??
                     throw new ArgumentNullException(nameof(configuration));

        var issuer = configuration["Jwt:Issuer"] ??
                     Environment.GetEnvironmentVariable("JWT_ISSUER") ??
                     throw new ArgumentNullException(nameof(configuration));

        var audience = configuration["Jwt:Audience"] ??
                       Environment.GetEnvironmentVariable("JWT_AUDIENCE") ??
                       throw new ArgumentNullException(nameof(configuration));

        return (secret, issuer, audience);
    }
}