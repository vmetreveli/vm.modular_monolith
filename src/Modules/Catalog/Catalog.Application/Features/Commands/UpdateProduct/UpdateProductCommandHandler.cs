using Catalog.Application.Contracts;
using Catalog.Domain.Entities;
using Catalog.Domain.Exception;
using Catalog.Domain.Repository;
using Catalog.Infrastructure.Context;
using Meadow_Framework.Core.Abstractions.Commands;
using Meadow_Framework.Core.Abstractions.Repository;

namespace Catalog.Application.Features.Commands.UpdateProduct;

internal class UpdateProductCommandHandler(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken = default)
    {
        //Update Product entity from command object
        //save to database
        //return result

        var product = await productRepository.GetByIdAsync(command.Product.Id, cancellationToken: cancellationToken);

        if (product is null)
        {
            throw new ProductNotFoundException(command.Product.Id);
        }

        UpdateProductWithNewValues(product, command.Product);

        await unitOfWork.CompleteAsync(cancellationToken);

        return new UpdateProductResult(true);
    }

    private void UpdateProductWithNewValues(Product product, ProductDto productDto)
    {
        product.Update(
            productDto.Name,
            productDto.Category,
            productDto.Description,
            productDto.ImageFile,
            productDto.Price);
    }
}
