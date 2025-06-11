using Catalog.Application.Contracts;
using Catalog.Domain.Repository;
using Catalog.Infrastructure.Context;
using Framework.Abstractions.Queries;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.Features.Queries.GetProductByCategory;

public class GetProductByCategoryQueryHandler(IProductRepository productRepository,CatalogDbContext dbContext)
    : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken = default)
    {
        // get products by category using dbContext
        // return result
        await productRepository.GetAllAsync(cancellationToken);
        var products = await dbContext.Products
                .AsNoTracking()
                .Where(p => p.Category.Contains(query.Category))
                .OrderBy(p => p.Name)
                .ToListAsync(cancellationToken);

        //mapping product entity to productdto
        var productDtos = products.Adapt<List<ProductDto>>();

        return new GetProductByCategoryResult { Products = productDtos };
    }
}
