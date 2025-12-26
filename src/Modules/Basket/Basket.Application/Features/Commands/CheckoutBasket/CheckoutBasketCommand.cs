using Basket.Application.Contracts;
using Meadow_Framework.Core.Abstractions.Commands;

namespace Basket.Application.Features.Commands.CheckoutBasket;

public record CheckoutBasketCommand(BasketCheckoutDto BasketCheckout)
    : ICommand<CheckoutBasketResult>;