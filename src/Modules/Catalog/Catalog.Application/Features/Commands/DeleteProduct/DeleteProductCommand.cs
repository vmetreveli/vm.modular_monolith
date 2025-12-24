using Meadow_Framework.Abstractions.Commands;

namespace Catalog.Application.Features.Commands.DeleteProduct;

public record DeleteProductCommand(Guid ProductId)
    : ICommand<DeleteProductResult>;