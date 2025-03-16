using App.Services.Orders.CreateOrderItem;
using FluentValidation;

namespace App.Services.Orders.Create;

public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequestDto>
{
    public CreateOrderRequestValidator()
    {
        RuleFor(x => x.OrderItems)
            .NotNull()
            .WithMessage("Order items cannot be null.")
            .NotEmpty()
            .WithMessage("At least one order item is required.");

        RuleForEach(x => x.OrderItems)
            .SetValidator(new CreateOrderItemRequestValidator());
    }
}

