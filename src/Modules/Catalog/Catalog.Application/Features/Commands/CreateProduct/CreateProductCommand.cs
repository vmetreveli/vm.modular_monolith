using Catalog.Application.Contracts;
using Meadow_Framework.Core.Abstractions.Commands;

namespace Catalog.Application.Features.Commands.CreateProduct;

public record CreateProductCommand(ProductDto Product)
    : ICommand<CreateProductResult>;