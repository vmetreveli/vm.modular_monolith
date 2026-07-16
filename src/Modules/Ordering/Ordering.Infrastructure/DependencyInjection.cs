using System.Reflection;
using Meadow_Framework.Core.Abstractions.Repository;
using Meadow_Framework.Core.Infrastructure.Interceptors;
using Meadow_Framework.Core.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ordering.Domain.Repository;
using Ordering.Infrastructure.Context;
using Ordering.Infrastructure.Repositories;

namespace Ordering.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<DomainEventEntitiesInterceptor>();
        services.AddScoped<UpdateAuditableEntitiesInterceptor>();
        services.AddScoped<UpdateDeletableEntitiesInterceptor>();

        services
            .AddDbContext<OrderingDbContext>((sp, options) =>
            {
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
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderUnitOfWork, OrderUnitOfWork>();

        return services;
    }
}