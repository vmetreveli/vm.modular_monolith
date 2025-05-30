using Catalog.Domain.Entities;
using Framework.Abstractions.Events;

namespace Catalog.Domain.Events;
public class ProductPriceChangedEvent : IDomainEvent
{
    public Product Product { get; init; } 
}
