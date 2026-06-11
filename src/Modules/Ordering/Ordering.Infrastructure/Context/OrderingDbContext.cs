using System.Reflection;
using Meadow_Framework.Core.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using Meadow_Framework.Core.Infrastructure.Context;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Context;

public class OrderingDbContext(DbContextOptions<BaseDbContext> options)
    : BaseDbContext(options) , IDbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("ordering");
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    #region Entities

    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    #endregion
}

public class ModularMonolithDbContextFactory : IDesignTimeDbContextFactory<BaseDbContext>
{
        public BaseDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BaseDbContext>();
            var conn = Environment.GetEnvironmentVariable("DefaultConnection");

            optionsBuilder
                .UseNpgsql(conn)
                .UseSnakeCaseNamingConvention();

            return new BaseDbContext(optionsBuilder.Options);
        }
}