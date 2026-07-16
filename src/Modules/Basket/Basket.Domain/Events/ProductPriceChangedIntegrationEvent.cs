using Meadow_Framework.Core.Abstractions.Events;
using Meadow_Framework.Core.Abstractions.Primitives;
using Meadow_Framework.Core.Infrastructure.Security;

// Shared contract namespace: MassTransit routes messages by namespace-qualified type name,
// so the publisher (Catalog) and consumer (Basket) copies must declare the same namespace.
namespace vm.modular.IntegrationEvents;

public class ProductPriceChangedIntegrationEvent : IntegrationBaseEvent
{
    public Guid ProductId { get; set; } = default!;
    public string Name { get; set; } = default!;
    public List<string> Category { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string ImageFile { get; set; } = default!;
    public decimal Price { get; set; } = default!;
}