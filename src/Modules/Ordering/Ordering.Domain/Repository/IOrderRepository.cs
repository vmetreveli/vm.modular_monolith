using Meadow_Framework.Abstractions.Repository;
using Ordering.Domain.Entities;

namespace Ordering.Domain.Repository;

public interface IOrderRepository : IRepositoryBase<Order, Guid>;