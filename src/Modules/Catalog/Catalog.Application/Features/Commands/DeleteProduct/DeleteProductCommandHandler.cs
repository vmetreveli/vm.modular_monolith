using Catalog.Domain.Exception;
using Catalog.Infrastructure.Context;
using Meadow_Framework.Core.Abstractions.Commands;

namespace Catalog.Application.Features.Commands.DeleteProduct;

internal class DeleteProductCommandHandler(CatalogDbContext dbContext)
    : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken = default)
    {
        //Delete Product entity from command object
        //save to database
        //return result

        var product = await dbContext.Products
           .FindAsync([command.ProductId], cancellationToken: cancellationToken);

        if (product is null)
        {
            throw new ProductNotFoundException(command.ProductId);
        }

        dbContext.Products.Remove(product);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteProductResult(true);
    }
}
