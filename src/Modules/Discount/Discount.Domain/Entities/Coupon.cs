using Meadow_Framework.Core.Abstractions.Primitives;

namespace Discount.Domain.Entities;

public class Coupon : AggregateRoot<Guid>, IAuditableEntity, IDeletableEntity
{
    public Coupon(Guid id) : base(id)
    {
    }

    public Coupon() : base(Guid.NewGuid())
    {

    }
    public string ProductName { get; set; }
    public string Description { get; set; }
    public int Amount { get; set; }
    public DateTime CreatedOn { get; }
    public DateTime ModifiedOn { get; }
    public bool IsDeleted { get; }
    public DateTime? DeletedOn { get; }
}