using Catalog.Domain.Entities;
using Meadow_Framework.Core.Abstractions.Events;

namespace Catalog.Domain.Events;
public class ProductCreatedEvent : IDomainEvent
{
    public Product Product { get; init; }
  
}
