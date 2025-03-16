using FluentValidation;

namespace App.Services.Orders.CreateOrderItem;

public class CreateOrderItemRequestValidator : AbstractValidator<CreateOrderItemRequestDto>
{
    public CreateOrderItemRequestValidator()
    {
        RuleFor(x => x.BookId)
            .GreaterThan(0)
            .WithMessage("Book ID must be greater than 0.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than 0.")
            .LessThanOrEqualTo(100)
            .WithMessage("Quantity cannot exceed 100 items per order.");
    }
}

