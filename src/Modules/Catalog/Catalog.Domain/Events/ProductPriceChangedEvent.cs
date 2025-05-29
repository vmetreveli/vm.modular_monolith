using Catalog.Domain.Entities;
using Framework.Abstractions.Events;

namespace Catalog.Domain.Events;
public record ProductPriceChangedEvent(Product Product)
    : IDomainEvent;
