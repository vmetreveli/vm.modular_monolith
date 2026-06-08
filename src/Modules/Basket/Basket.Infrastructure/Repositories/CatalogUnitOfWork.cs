using Basket.Domain.Repository;
using Basket.Infrastructure.Context;
using Meadow_Framework.Core.Infrastructure.Repository;

namespace Basket.Infrastructure.Repositories;


public class BasketUnitOfWork(BasketDbContext dbContext)
    : UnitOfWork<BasketDbContext>(dbContext), IBasketUnitOfWork;