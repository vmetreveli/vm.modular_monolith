using Basket.Application.Contracts;
using Framework.Abstractions.Commands;

namespace Basket.Application.Features.Commands.CheckoutBasket;

public record CheckoutBasketCommand(BasketCheckoutDto BasketCheckout)
    : ICommand<CheckoutBasketResult>;