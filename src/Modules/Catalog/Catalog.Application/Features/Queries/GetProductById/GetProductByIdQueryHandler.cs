using Catalog.Application.Contracts;
using Catalog.Domain.Exception;
using Catalog.Domain.Repository;
using Framework.Abstractions.Queries;

namespace Catalog.Application.Features.Queries.GetProductById;


public class GetProductByIdQueryHandler(IProductRepository productRepository) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken = default)
    {
        // get products by id using dbContext
        // return result

        var product = await productRepository.GetByIdAsync(query.Id, cancellationToken);

        if (product is null)
        {
            throw new ProductNotFoundException(query.Id);
        }

        //mapping product entity to productdto
        var productDto = new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Category = product.Category,
            Description = product.Description,
            ImageFile = product.ImageFile,
            Price = product.Price
        };

        return new GetProductByIdResult {Product = productDto};
    }
}
