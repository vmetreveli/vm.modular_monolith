using Catalog.Domain.Events;
using Framework.Abstractions.Primitives;

namespace Catalog.Domain.Entities;
public class Product : AggregateRoot<Guid>
{
    public Product(Guid id, string name, List<string> category, string description, string imageFile, decimal price) : base(id)
    {
        Name = name;
        Description = description;
        ImageFile = imageFile;
        Price = price;
    }

    public string Name { get; private set; } = default!;
    public List<string> Category { get; private set; } = new();
    public string Description { get; private set; } = default!;
    public string ImageFile { get; private set; } = default!;
    public decimal Price { get; private set; }

    public static Product Create(Guid id, string name, List<string> category, string description, string imageFile, decimal price)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

        var product = new Product(
            id,
            name,
            category,
            description,
            imageFile,
            price
        );

        product.RaiseDomainEvent(new ProductCreatedEvent{Product = product});

        return product;
    }

    public void Update(string name, List<string> category, string description, string imageFile, decimal price)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

        // Update Product entity fields
        Name = name;
        Category = category;
        Description = description;
        ImageFile = imageFile;        

        // if price has changed, raise ProductPriceChanged domain event
        if (Price != price)
        {
            Price = price;
            RaiseDomainEvent(new ProductPriceChangedEvent{Product = this});
        }
    }
}
