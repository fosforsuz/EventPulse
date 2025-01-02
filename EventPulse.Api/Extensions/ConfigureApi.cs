using EventPulse.Application.Extensions;

namespace EventPulse.Api.Extensions;

public static class ConfigureApi
{
    public static void ConfigureModules(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureApplicationAndInfrastructureServices(configuration);
        services.ConfigureMediatR();
    }

    private static void ConfigureMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
    }
}