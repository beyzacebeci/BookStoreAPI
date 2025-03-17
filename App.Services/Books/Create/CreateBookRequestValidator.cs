using FluentValidation;

namespace App.Services.Books.Create;

public class CreateBookRequestValidator : AbstractValidator<CreateBookRequestDto>
{
    public CreateBookRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotNull().WithMessage("Book title cannot be null.")
            .NotEmpty().WithMessage("Book title cannot be empty.");

        RuleFor(x => x.Author)
            .NotNull().WithMessage("Author cannot be null.")
            .NotEmpty().WithMessage("Author cannot be empty.");

        RuleFor(x => x.ISBN)
           .NotNull().WithMessage("ISBN cannot be null.")
           .NotEmpty().WithMessage("ISBN cannot be empty.");

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Price cannot be negative.");

        RuleFor(x => x.StockQuantity)
            .GreaterThanOrEqualTo(0).WithMessage("Stock quantity cannot be negative.");

        RuleFor(x => x.PublicationYear)
            .InclusiveBetween(1450, DateTime.UtcNow.Year)
            .WithMessage($"Publication year must be between 1450 and {DateTime.UtcNow.Year}.");
    }
}

