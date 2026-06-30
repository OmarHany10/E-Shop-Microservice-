using Basket.API.Data;
using BuildingBlocks.CQRS;
using FluentValidation;

namespace Basket.API.Basket.DeleteBasket
{
    public record DeleteBasketCommand(string userName): ICommand<DeleteBasketResult>;
    public record DeleteBasketResult(bool isDeleted);

    public class DeleteBasketCommandValidator: AbstractValidator<DeleteBasketCommand>
    {
        public DeleteBasketCommandValidator()
        {
            RuleFor(c => c.userName).NotEmpty().WithMessage("UserName is required");
        }
    }
    public class DeleteBasketHandler(IBasketRepository basketRepository)
        : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
        {
            var result = await basketRepository.DeleteBasket(request.userName, cancellationToken);

            return new DeleteBasketResult(result);
        }
    }
}
