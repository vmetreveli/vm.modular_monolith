using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Context.Configurations;

public class ProductConfig: IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name).HasMaxLength(50).IsRequired();

        builder.Property(p => p.Category).IsRequired();

        builder.Property(p => p.Description).HasMaxLength(200);

        builder.Property(p => p.ImageFile).HasMaxLength(100);

        builder.Property(p => p.Price).IsRequired();
    }
}