using Meadow_Framework.Abstractions.Events;
using Meadow_Framework.Abstractions.Primitives;

namespace Basket.Domain.Events;

public class ProductPriceChangedIntegrationEvent : IntegrationBaseEvent
{
    public Guid ProductId { get; set; } = default!;
    public string Name { get; set; } = default!;
    public List<string> Category { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string ImageFile { get; set; } = default!;
    public decimal Price { get; set; } = default!;
}