using Basket.Domain.Repository;
using Basket.Infrastructure.Context;
using Basket.Infrastructure.Repositories;
using Basket.Infrastructure.Services.Catalog;
using Framework.Abstractions.Repository;
using Framework.Infrastructure.Interceptors;
using Framework.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace Basket.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<InsertOutboxMessagesInterceptor>();
        services.AddScoped<UpdateAuditableEntitiesInterceptor>();
        services.AddScoped<UpdateDeletableEntitiesInterceptor>();

        services
            .AddDbContext<BasketDbContext>((sp, options) =>
            {
                var outboxMessagesInterceptor = sp.GetService<InsertOutboxMessagesInterceptor>();
                var auditableInterceptor = sp.GetService<UpdateAuditableEntitiesInterceptor>();
                var deletableEntitiesInterceptor = sp.GetService<UpdateDeletableEntitiesInterceptor>();

                options.UseNpgsql(
                        configuration.GetConnectionString("DefaultConnection"))
                    // options =>
                    // {
                    //     options.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                    //     options.MigrationsHistoryTable($"__{nameof(NotificationDbContext)}");
                    //
                    //     options.EnableRetryOnFailure(5);
                    //     options.MinBatchSize(1);
                    // })
                    .UseSnakeCaseNamingConvention()
                    .AddInterceptors(outboxMessagesInterceptor!)
                    .AddInterceptors(auditableInterceptor!)
                    .AddInterceptors(deletableEntitiesInterceptor!)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors();
            });

        //services.AddScoped<IEventRepository, EventRepository>();
        //  services.AddScoped<IEventDictionaryRepository, EventDictionaryRepository>();
        services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork<BasketDbContext>>();
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