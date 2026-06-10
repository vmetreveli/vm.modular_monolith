using System.Reflection;
using Basket.Domain.Entities;
using Meadow_Framework.Core.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using Meadow_Framework.Core.Infrastructure.Context;

namespace Basket.Infrastructure.Context;

public class BasketDbContext(DbContextOptions<BaseDbContext> options)
    : BaseDbContext(options), IDbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("basket");
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    #region Entities

    public DbSet<ShoppingCart> ShoppingCarts => Set<ShoppingCart>();
    public DbSet<ShoppingCartItem> ShoppingCartItems => Set<ShoppingCartItem>();

    #endregion
}

public class ModularMonolithDbContextFactory : IDesignTimeDbContextFactory<BasketDbContext>
{
        public BasketDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BaseDbContext>();
            var conn = Environment.GetEnvironmentVariable("BasketConnection");

            optionsBuilder
                .UseNpgsql(conn)
                .UseSnakeCaseNamingConvention();

            return new BasketDbContext(optionsBuilder.Options);
        }
}