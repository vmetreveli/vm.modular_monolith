using Catalog.Application;
using Catalog.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Module;

public static class DependencyInjection
{
    public static IServiceCollection AddCatalogModule(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddApplication(configuration);
        services.AddInfrastructure(configuration);
        return services;
    }
    
}