using Catalog.Application.Contracts;
using Catalog.Domain.Entities;
using Catalog.Domain.Repository;
using Meadow_Framework.Core.Abstractions.Commands;
using Meadow_Framework.Core.Abstractions.Repository;

namespace Catalog.Application.Features.Commands.CreateProduct;

public class CreateProductCommandHandler(IProductRepository productRepository, ICatalogUnitOfWork unitOfWork) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, 
        CancellationToken cancellationToken = default)
    {
        //create Product entity from command object
        //save to database
        //return result
        
        var product = CreateNewProduct(command.Product);

        await productRepository.AddAsync(product, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);

        return new CreateProductResult(product.Id);
    }

    private Product CreateNewProduct(ProductDto productDto)
    {
        var product = Product.Create(
            Guid.NewGuid(),
            productDto.Name,
            productDto.Category,
            productDto.Description,
            productDto.ImageFile,
            productDto.Price);

        return product;
    }
}
