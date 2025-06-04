using Basket.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Basket.Infrastructure.Context.Configurations;

public class ShoppingCartItemConfig : IEntityTypeConfiguration<ShoppingCartItem>
{
    public void Configure(EntityTypeBuilder<ShoppingCartItem> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(oi => oi.ProductId).IsRequired();

        builder.Property(oi => oi.Quantity).IsRequired();

        builder.Property(oi => oi.Color);

        builder.Property(oi => oi.Price).IsRequired();

        builder.Property(oi => oi.ProductName).IsRequired();
        
        builder.Property(c => c.CreatedOn).IsRequired();
        builder.Property(c => c.ModifiedOn);
        builder.Property(c => c.DeletedOn);
        builder.Property(c => c.IsDeleted).IsRequired();

        builder.HasQueryFilter(c => !c.IsDeleted);

    }
}
