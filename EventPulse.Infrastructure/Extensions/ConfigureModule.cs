using EventPulse.Infrastructure.Context;
using EventPulse.Infrastructure.Interfaces;
using EventPulse.Infrastructure.Persistence;
using EventPulse.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EventPulse.Infrastructure.Extensions;

public static class ConfigureModule
{
    public static void ConfigureInfrastructure(this IServiceCollection services, string connectionString, string secret,
        string issuer, string audience, bool useInMemory = false)
    {
        services.ConfigureDbContext(connectionString, useInMemory);
        services.ConfigureRepositories();
        services.ConfigureJwtServices(secret, issuer, audience);
        services.ConfigureUnitOfWork();
    }

    private static void ConfigureDbContext(this IServiceCollection services, string connectionString, bool useInMemory)
    {
        if (useInMemory)
            services.AddDbContext<EventPulseContext>(options => options.UseInMemoryDatabase("TestDB"));
        else
            services.AddDbContext<EventPulseContext>(options =>
            {
                options.UseSqlServer(connectionString);

                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                    options.EnableSensitiveDataLogging();
            });
    }

    private static void ConfigureRepositories(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssemblies(typeof(IRepository<>).Assembly)
            .AddClasses(classes => classes.AssignableTo(typeof(IRepository<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime()
        );
    }

    private static void ConfigureJwtServices(this IServiceCollection services, string secret, string issuer,
        string audience)
    {
        services.AddSingleton<IJwtService>(new JwtService(
            secret,
            issuer,
            audience
        ));
    }

    private static void ConfigureUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}