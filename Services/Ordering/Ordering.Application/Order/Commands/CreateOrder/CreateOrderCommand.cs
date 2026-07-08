using BuildingBlocks.CQRS;
using FluentValidation;
using Ordering.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Application.Order.Commands.CreateOrder
{
    public record CreateOrderCommand(OrderDTO OrderDTO): ICommand<CreateOrderResult>;

    public record CreateOrderResult(Guid Id);

    public class CreateOrderValidator: AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderValidator()
        {
            RuleFor(x => x.OrderDTO.OrderName).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.OrderDTO.CustomerId).NotNull().WithMessage("CustomerId is required");
            RuleFor(x => x.OrderDTO.OrderItems).NotEmpty().WithMessage("OrderItems should not be empty");
        }
    }
}
