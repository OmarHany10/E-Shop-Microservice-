using Basket.API.Data;
using Basket.API.Models;
using BuildingBlocks.CQRS;
using FluentValidation;

namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppinCart Cart): ICommand<StoreBasketResult>;
    public record StoreBasketResult(string userName);

    public class StoreBasketCommandValidator: AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(s => s.Cart).NotNull().WithMessage("Cart must not be null");
            RuleFor(s => s.Cart.UserName).NotEmpty().WithMessage("UserName must not be empty");
        }
    }
    public class StoreBasketHandler(IBasketRepository basketRepository)
        : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
        {
            ShoppinCart shoppinCart = await basketRepository.StoreBasket(request.Cart);

            return new StoreBasketResult(shoppinCart.UserName);
        }
    }
}
