using Catalog.Domain.Repository;
using Catalog.Infrastructure.Context;
using Meadow_Framework.Core.Infrastructure.Repository;

namespace Catalog.Infrastructure.Repositories;


public class CatalogUnitOfWork(CatalogDbContext dbContext)
    : UnitOfWork<CatalogDbContext>(dbContext), ICatalogUnitOfWork;