using System.Reflection;
using Catalog.Domain.Entities;
using Meadow_Framework.Core.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Meadow_Framework.Core.Infrastructure.Context;

namespace Catalog.Infrastructure.Context;

public class CatalogDbContext(DbContextOptions<BaseDbContext> options)
    : BaseDbContext(options), IDbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("catalog");
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    #region Entities

    public DbSet<Product> Products => Set<Product>();
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