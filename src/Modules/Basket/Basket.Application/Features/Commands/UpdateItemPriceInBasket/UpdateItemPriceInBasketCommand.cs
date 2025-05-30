using System;
using Basket.Application.Contracts;
using Framework.Abstractions.Commands;

namespace Basket.Application.Features.Commands.UpdateItemPriceInBasket;

public record UpdateItemPriceInBasketCommand(Guid ProductId, decimal Price)
    : ICommand<UpdateItemPriceInBasketResult>;