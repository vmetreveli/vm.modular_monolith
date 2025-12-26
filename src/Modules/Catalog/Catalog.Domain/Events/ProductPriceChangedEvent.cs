using Catalog.Domain.Entities;
using Meadow_Framework.Core.Abstractions.Events;

namespace Catalog.Domain.Events;
public class ProductPriceChangedEvent : IDomainEvent
{
    public Product Product { get; init; } 
}
