using System.Reflection;
using Basket.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Basket.Infrastructure.Context;

public class BasketDbContext(DbContextOptions<BasketDbContext> options)
    : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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
        var optionsBuilder = new DbContextOptionsBuilder<BasketDbContext>();
        optionsBuilder
            .UseNpgsql("DefaultConnection")
            .UseSnakeCaseNamingConvention();

        return new BasketDbContext(optionsBuilder.Options);
    }
}