using MapsterMapper;
using Meadow_Framework.Core.Abstractions.Commands;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;
using Ordering.Domain.Repository;

namespace Ordering.Application.Features.Commands.CheckoutOrder;

public class CheckoutOrderCommandHandler : ICommandHandler<CheckoutOrderCommand, string>
{
    private readonly ILogger<CheckoutOrderCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IOrderRepository _orderRepository;

    public CheckoutOrderCommandHandler(IMapper mapper,
        IOrderRepository orderRepository,
        ILogger<CheckoutOrderCommandHandler> logger)
    {
        _mapper = mapper;
        _orderRepository = orderRepository;
        _logger = logger;
    }

    public async Task<string> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken = default)
    {
        var orderEntity = _mapper.Map<Order>(request);
        await _orderRepository.AddAsync(orderEntity, cancellationToken);
        _logger.LogInformation("Order {Id} is successfully created.", orderEntity.Id);
        return orderEntity.Id.ToString();
    }
}