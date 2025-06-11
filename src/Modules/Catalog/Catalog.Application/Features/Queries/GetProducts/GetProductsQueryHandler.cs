using Catalog.Application.Contracts;
using Catalog.Application.Contracts.Pagination;
using Catalog.Domain.Repository;
using Framework.Abstractions.Queries;

namespace Catalog.Application.Features.Queries.GetProducts;

public class GetProductsQueryHandler(IProductRepository productRepository) : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken = default)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var (products, totalCount) = await productRepository.GetPaginatedAsync(
            pageIndex,
            pageSize,
            null,
            cancellationToken);

        var productDtos = products.Select(i => new ProductDto
        {
            Id = i.Id,
            Name = i.Name,
            Category = i.Category,
            Description = i.Description,
            ImageFile = i.ImageFile,
            Price = i.Price,

        }).ToList();


        return new GetProductsResult(
            new PaginatedResult<ProductDto>(
                pageIndex,
                pageSize,
                totalCount,
                productDtos)
        );
    }

}
