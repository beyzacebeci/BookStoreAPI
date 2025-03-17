using FluentValidation;

namespace App.Services.Categories.Create;

public class CreateCategoryRequestValidator : AbstractValidator<CreateCategoryRequestDto>
{
    public CreateCategoryRequestValidator()
    {
        RuleFor(x => x.Name)
    .NotNull().WithMessage("Cateory name cannot be null.")
    .NotEmpty().WithMessage("Category name cannot be empty.");
    }
}

