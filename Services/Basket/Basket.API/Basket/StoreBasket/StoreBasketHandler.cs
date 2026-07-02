using Basket.API.Data;
using Basket.API.Models;
using BuildingBlocks.CQRS;
using Discount.gRPC;
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
    public class StoreBasketHandler(IBasketRepository basketRepository, DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient)
        : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
        {
            // gRPC call
            foreach(var item in request.Cart.Items)
            {
                var coupon = await discountProtoServiceClient.GetDiscountAsync(new GetDiscountRequest() { ProductName=item.ProductName});
                item.Price -= coupon.Amount;
            }

            ShoppinCart shoppinCart = await basketRepository.StoreBasket(request.Cart);

            return new StoreBasketResult(shoppinCart.UserName);
        }



    }
}
