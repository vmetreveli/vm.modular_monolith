using Catalog.Application.Contracts;
using Meadow_Framework.Abstractions.Commands;

namespace Catalog.Application.Features.Commands.CreateProduct;

public record CreateProductCommand(ProductDto Product)
    : ICommand<CreateProductResult>;