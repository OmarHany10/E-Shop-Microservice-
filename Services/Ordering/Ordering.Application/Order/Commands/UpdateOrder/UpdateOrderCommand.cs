using BuildingBlocks.CQRS;
using FluentValidation;
using Ordering.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Application.Order.Commands.UpdateOrder
{
    public record UpdateOrderCommand(OrderDTO OrderDTO) : ICommand<UpdateOrderResult>;

    public record UpdateOrderResult(bool isUpdated);

    public class UpdateOrderValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderValidator()
        {
            RuleFor(x => x.OrderDTO.OrderName).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.OrderDTO.OrderId).NotNull().WithMessage("Id is required");
            RuleFor(x => x.OrderDTO.CustomerId).NotNull().WithMessage("CustomerId is required");
            RuleFor(x => x.OrderDTO.OrderItems).NotEmpty().WithMessage("OrderItems should not be empty");
        }
    }
}
