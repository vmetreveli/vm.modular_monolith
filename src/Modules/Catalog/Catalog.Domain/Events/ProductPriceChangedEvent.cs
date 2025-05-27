using Catalog.Domain.Entities;
using Framework.Abstractions.Events;

namespace Catalog.Application.Features.Events;
public record ProductPriceChangedEvent(Product Product)
    : IDomainEvent;
