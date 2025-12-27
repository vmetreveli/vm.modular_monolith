using Catalog.Domain.Repository;
using Catalog.Infrastructure.Context;
using Meadow_Framework.Core.Abstractions.Repository;
using Meadow_Framework.Core.Infrastructure.Repository;

namespace Catalog.Infrastructure.Repositories;

public class CatalogUnitOfWork(CatalogDbContext context) : UnitOfWork<CatalogDbContext>(context), ICatalogUnitOfWork;