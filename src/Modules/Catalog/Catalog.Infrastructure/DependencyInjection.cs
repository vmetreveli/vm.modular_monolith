using System.Reflection;
using Catalog.Domain.Repository;
using Catalog.Infrastructure.Context;
using Catalog.Infrastructure.Repositories;
using Meadow_Framework.Core.Infrastructure.Interceptors;
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
                InsertOutboxMessagesInterceptor? outboxMessagesInterceptor = sp.GetService<InsertOutboxMessagesInterceptor>();
                UpdateAuditableEntitiesInterceptor? auditableInterceptor = sp.GetService<UpdateAuditableEntitiesInterceptor>();
                UpdateDeletableEntitiesInterceptor? deletableEntitiesInterceptor = sp.GetService<UpdateDeletableEntitiesInterceptor>();

                options.UseNpgsql(
                        configuration.GetConnectionString("DefaultConnection"),
                    options =>
                    {
                        options.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                        options.MigrationsHistoryTable($"__{nameof(CatalogDbContext)}");

                        options.EnableRetryOnFailure(5);
                        options.MinBatchSize(1);
                    })
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
        services.AddScoped<ICatalogUnitOfWork, CatalogUnitOfWork>();

        return services;
    }

}