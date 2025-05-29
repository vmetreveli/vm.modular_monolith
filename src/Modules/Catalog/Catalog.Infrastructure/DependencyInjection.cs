using Catalog.Domain.Repository;
using Catalog.Infrastructure.Context;
using Catalog.Infrastructure.Repositories;
using Framework.Abstractions.Repository;
using Framework.Infrastructure;
using Framework.Infrastructure.Interceptors;
using Framework.Infrastructure.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<InsertOutboxMessagesInterceptor>();
        services.AddScoped<UpdateAuditableEntitiesInterceptor>();
        services.AddScoped<UpdateDeletableEntitiesInterceptor>();

        services
            .AddDbContext<CatalogDbContext>((sp, options) =>
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
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork<CatalogDbContext>>();

        return services;
    }

    // public static IApplicationBuilder UseBasketModule(this IApplicationBuilder app)
    // {
    //     // Configure the HTTP request pipeline.
    //     // 1. Use Api Endpoint services
    //
    //     // 2. Use Application Use Case services
    //
    //     // 3. Use Data - Infrastructure services
    //     app.UseMigration<CatalogDbContext>();
    //
    //     return app;
    // }
}