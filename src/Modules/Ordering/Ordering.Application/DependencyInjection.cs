using Framework.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(DependencyInjection).Assembly);
        services.AddFramework(configuration, typeof(DependencyInjection).Assembly);
        // services.AddAsynchronousAdapter(configuration);
        // services.AddApplicationServices(configuration);
        // services.AddDomainEventsHandlers(typeof(DependencyInjection).Assembly);

        return services;
    }
}