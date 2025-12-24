using Catalog.Application.Contracts;
using Meadow_Framework.Abstractions.Commands;

namespace Catalog.Application.Features.Commands.UpdateProduct;

public record UpdateProductCommand(ProductDto Product)
    : ICommand<UpdateProductResult>;