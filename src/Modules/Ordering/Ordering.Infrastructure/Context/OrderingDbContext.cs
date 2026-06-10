using System.Reflection;
using Meadow_Framework.Core.Abstractions;
using Meadow_Framework.Core.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Context;

public class OrderingDbContext(DbContextOptions<BaseDbContext> options)
    : BaseDbContext(options) , IDbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    #region Entities

    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    #endregion
}

public class ModularMonolithDbContextFactory : IDesignTimeDbContextFactory<OrderingDbContext>
{
    public OrderingDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<BaseDbContext>();
        optionsBuilder
            .UseNpgsql("DefaultConnection")
            .UseSnakeCaseNamingConvention();

        return new OrderingDbContext(optionsBuilder.Options);
    }
}