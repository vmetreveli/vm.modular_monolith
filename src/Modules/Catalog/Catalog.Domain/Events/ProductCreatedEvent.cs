using Catalog.Domain.Entities;
using Framework.Abstractions.Events;

namespace Catalog.Domain.Events;
public class ProductCreatedEvent : IDomainEvent
{
    public Product Product { get; init; }
  
}
