using Basket.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Basket.Infrastructure.Context.Configurations;

public class ShoppingCartConfig : IEntityTypeConfiguration<ShoppingCart>
{
    public void Configure(EntityTypeBuilder<ShoppingCart> builder)
    {
        builder.HasKey(e => e.Id);

        // builder.HasIndex(e => e.UserName)
        //     .IsUnique();
        //
        // builder.Property(e => e.UserName)
        //     .IsRequired()
        //     .HasMaxLength(100);

        builder.HasMany(s => s.Items)
            .WithOne()
            .HasForeignKey(si => si.ShoppingCartId);
        
        builder.Property(c => c.CreatedOn).IsRequired();
        builder.Property(c => c.ModifiedOn);
        builder.Property(c => c.DeletedOn);
        builder.Property(c => c.IsDeleted).IsRequired();

        builder.HasQueryFilter(c => !c.IsDeleted);
    }
}
