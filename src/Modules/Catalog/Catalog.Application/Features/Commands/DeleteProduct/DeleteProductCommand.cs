using Meadow_Framework.Core.Abstractions.Commands;

namespace Catalog.Application.Features.Commands.DeleteProduct;

public record DeleteProductCommand(Guid ProductId)
    : ICommand<DeleteProductResult>;