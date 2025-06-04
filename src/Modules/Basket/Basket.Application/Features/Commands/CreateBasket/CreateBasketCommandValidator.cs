using FluentValidation;

namespace Basket.Application.Features.Commands.CreateBasket;

public class CreateBasketCommandValidator : AbstractValidator<CreateBasketCommand>
{
    public CreateBasketCommandValidator()
    {
     //   RuleFor(x => x.ShoppingCart.UserName).NotEmpty().WithMessage("UserName is required");
    }
}