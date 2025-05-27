using System.Reflection;
using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Catalog.Infrastructure.Context;

public class CatalogDbContext(DbContextOptions<CatalogDbContext> options)
    : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    #region Entities

    public DbSet<Product> Products => Set<Product>();
    #endregion
}

public class ModularMonolithDbContextFactory : IDesignTimeDbContextFactory<CatalogDbContext>
{
    public CatalogDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CatalogDbContext>();
        optionsBuilder
            .UseNpgsql("DefaultConnection")
            .UseSnakeCaseNamingConvention();

        return new CatalogDbContext(optionsBuilder.Options);
    }
}