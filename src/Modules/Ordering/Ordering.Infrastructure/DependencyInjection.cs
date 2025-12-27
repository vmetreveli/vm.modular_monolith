using Meadow_Framework.Core.Abstractions.Repository;
using Meadow_Framework.Core.Infrastructure.Interceptors;
using Meadow_Framework.Core.Infrastructure.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Domain.Entities;
using Ordering.Domain.Repository;
using Ordering.Infrastructure.Context;
using Ordering.Infrastructure.Repositories;

namespace Ordering.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<InsertOutboxMessagesInterceptor>();
        services.AddScoped<UpdateAuditableEntitiesInterceptor>();
        services.AddScoped<UpdateDeletableEntitiesInterceptor>();

        services
            .AddDbContext<OrderingDbContext>((sp, options) =>
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
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderUnitOfWork, OrderUnitOfWork>();
        services.AddScoped<IUnitOfWork, UnitOfWork<OrderingDbContext>>();

        return services;
    }
}