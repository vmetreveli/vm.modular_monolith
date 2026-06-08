using System.Reflection;
using Meadow_Framework.Core.Infrastructure.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
                        configuration.GetConnectionString("DefaultConnection"),
                    options =>
                    {
                        options.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                        options.MigrationsHistoryTable($"__{nameof(OrderingDbContext)}");

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
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderUnitOfWork, OrderUnitOfWork>();

        return services;
    }
}