using System.Data;
using Basket.Domain.Repository;
using Basket.Infrastructure.Context;
using Meadow_Framework.Core.Abstractions.Repository;
using Meadow_Framework.Core.Infrastructure.Repository;

namespace Basket.Infrastructure.Repositories;

public class BasketUnitOfWork(BasketDbContext context) : UnitOfWork<BasketDbContext>(context), IBasketUnitOfWork;