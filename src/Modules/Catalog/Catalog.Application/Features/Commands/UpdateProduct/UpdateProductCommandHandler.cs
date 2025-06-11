using Catalog.Application.Contracts;
using Catalog.Domain.Entities;
using Catalog.Domain.Exception;
using Catalog.Infrastructure.Context;
using Framework.Abstractions.Commands;

namespace Catalog.Application.Features.Commands.UpdateProduct;

internal class UpdateProductCommandHandler(CatalogDbContext dbContext)
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken = default)
    {
        //Update Product entity from command object
        //save to database
        //return result

        var product = await dbContext.Products
          .FindAsync([command.Product.Id], cancellationToken: cancellationToken);

        if (product is null)
        {
            throw new ProductNotFoundException(command.Product.Id);
        }

        UpdateProductWithNewValues(product, command.Product);

        dbContext.Products.Update(product);
        await dbContext.SaveChangesAsync(cancellationToken);

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
