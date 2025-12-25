using Basket.Domain.Entities;
using Meadow_Framework.Abstractions.Specifications;


namespace Basket.Infrastructure.Specifications;

/// <summary>
///     Represents the specification for determining the user with email.
/// </summary>
public sealed class ShoppingCartWithItemSpecification : Specification<ShoppingCart, Guid>
{
    public ShoppingCartWithItemSpecification(Guid productId) : base(order => order.IsDeleted == false) =>
        AddInclude(order => order.Items.Where(i=>i.ProductId == productId));
    
    public ShoppingCartWithItemSpecification(string userName) : base(order => order.UserName==userName) =>
        AddInclude(order => order.Items);

}