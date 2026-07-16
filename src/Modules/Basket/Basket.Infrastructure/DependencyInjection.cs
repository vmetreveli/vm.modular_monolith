using System.Reflection;
using Basket.Domain.Repository;
using Basket.Infrastructure.Context;
using Basket.Infrastructure.Repositories;
using Basket.Infrastructure.Services.Catalog;
using Meadow_Framework.Core.Abstractions.Repository;
using Meadow_Framework.Core.Infrastructure.Interceptors;
using Meadow_Framework.Core.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Refit;

namespace Basket.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<DomainEventEntitiesInterceptor>();
        services.AddScoped<UpdateAuditableEntitiesInterceptor>();
        services.AddScoped<UpdateDeletableEntitiesInterceptor>();

        services
            .AddDbContext<BasketDbContext>((sp, options) =>
            {
                options.UseNpgsql(
                        configuration.GetConnectionString("DefaultConnection"),
                    options =>
                    {
                        options.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                        options.MigrationsHistoryTable($"__{nameof(BasketDbContext)}");

                        options.EnableRetryOnFailure(5);
                        options.MinBatchSize(1);
                    })
                    .UseSnakeCaseNamingConvention()
                    .AddInterceptors(
                        sp.GetRequiredService<DomainEventEntitiesInterceptor>(),
                        sp.GetRequiredService<UpdateAuditableEntitiesInterceptor>(),
                        sp.GetRequiredService<UpdateDeletableEntitiesInterceptor>());

                if (sp.GetRequiredService<IHostEnvironment>().IsDevelopment())
                {
                    options.EnableSensitiveDataLogging()
                        .EnableDetailedErrors();
                }
            });
        //services.AddScoped<IEventRepository, EventRepository>();
        //  services.AddScoped<IEventDictionaryRepository, EventDictionaryRepository>();
        services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
        services.AddScoped<IBasketUnitOfWork, BasketUnitOfWork>();
       // services.AddScoped<IProductReadService, ProductReadService>();
       AddCatalogApiClient(services,configuration);
        return services;
    }
    
    
    private static void AddCatalogApiClient(IServiceCollection services, IConfiguration configuration)
    {
        var baseAddress = configuration["AppConfiguration:ExternalServices:CatalogApi:BaseAddress"];
       baseAddress.ThrowIfNullOrEmpty();

        services.AddRefitClient<IProductReadService>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(baseAddress));
    }


  
}