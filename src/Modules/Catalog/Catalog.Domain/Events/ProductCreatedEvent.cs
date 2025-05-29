using Catalog.Domain.Entities;
using Framework.Abstractions.Events;

namespace Catalog.Domain.Events;
public record ProductCreatedEvent(Product Product)
    : IDomainEvent;
