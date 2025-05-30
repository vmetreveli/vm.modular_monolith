using Framework.Abstractions.Queries;
using Ordering.Application.Contracts;
using Ordering.Application.Contracts.Pagination;
using Ordering.Domain.Repository;
using Ordering.Infrastructure.Context;

namespace Ordering.Application.Features.Queries.GetOrders;

public class GetOrdersQueryHandler(IOrderRepository orderRepository) : IQueryHandler<GetOrdersQuery, GetOrdersResult>
{
    public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
    {

        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

       
        var (orders, totalCount) = await orderRepository.GetPaginatedAsync(pageIndex, pageSize,null, cancellationToken);


        var orderDtos = orders.Select(p=>new OrderDto
        {
            Id = p.Id,
            CustomerId = p.CustomerId,
            OrderName = p.OrderName,
            ShippingAddress = new AddressDto
            {
                FirstName = p.ShippingAddress.FirstName,
                LastName = p.ShippingAddress.LastName,
                EmailAddress = p.ShippingAddress.EmailAddress,
                AddressLine = p.ShippingAddress.AddressLine,
                Country = p.ShippingAddress.Country,
                State = p.ShippingAddress.State,
                ZipCode = p.ShippingAddress.ZipCode
            },
            BillingAddress = new AddressDto
            {
                FirstName = p.BillingAddress.FirstName,
                LastName = p.BillingAddress.LastName,
                EmailAddress = p.BillingAddress.EmailAddress,
                AddressLine = p.BillingAddress.AddressLine,
                Country = p.BillingAddress.Country,
                State = p.BillingAddress.State,
                ZipCode = p.BillingAddress.ZipCode
            },
            Payment = new PaymentDto
            {
                CardName = p.Payment.CardName,
                CardNumber = p.Payment.CardNumber,
                Expiration = p.Payment.Expiration,
                Cvv = p.Payment.CVV,
                PaymentMethod = p.Payment.PaymentMethod
            },
            Items = p.Items.Select(i => new OrderItemDto
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                Price = i.Price
            }).ToList()
        }).ToList();
        
        return new GetOrdersResult(
            new PaginatedResult<OrderDto>(
                pageIndex,
                pageSize,
                totalCount,
                orderDtos));
    }
}
