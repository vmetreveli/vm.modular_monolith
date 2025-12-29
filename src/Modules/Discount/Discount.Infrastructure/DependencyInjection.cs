using Discount.Domain.Repository;
using Discount.Infrastructure.Context;
using Discount.Infrastructure.Repositories;
using Meadow_Framework.Core.Infrastructure.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Discount.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<InsertOutboxMessagesInterceptor>();
        services.AddScoped<UpdateAuditableEntitiesInterceptor>();
        services.AddScoped<UpdateDeletableEntitiesInterceptor>();

        services
            .AddDbContext<DiscountDbContext>((sp, options) =>
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
        services.AddScoped<IDiscountRepository, DiscountRepository>();
        //   services.AddScoped<ICatalogUnitOfWork, CatalogUnitOfWork>();

        return services;
    }
}