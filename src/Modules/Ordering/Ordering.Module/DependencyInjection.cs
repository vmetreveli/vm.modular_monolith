using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application;
using Ordering.Infrastructure;

namespace Ordering.Module;

public static class DependencyInjection
{
    public static IServiceCollection AddOrderingModule(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddApplication(configuration);
        services.AddInfrastructure(configuration);
        return services;
    }
    
}