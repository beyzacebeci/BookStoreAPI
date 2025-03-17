namespace App.Services.Categories.Create;

public record CreateCategoryRequestDto
{
    public string Name { get; init; } = default!;

}

