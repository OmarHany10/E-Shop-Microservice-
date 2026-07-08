using BuildingBlocks.CQRS;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Ordering.Application.Order.Commands.DeleteOrder
{
    public record DeleteOrderCommand(Guid Id) : ICommand<DeleteOrderResult>;
    public record DeleteOrderResult(bool isDeleted);

    public class DeleteOrderValidator : AbstractValidator<DeleteOrderCommand>
    {
        public DeleteOrderValidator()
        {
            RuleFor(x => x.Id).NotNull().WithMessage("Id is required");
        }
    }
}
