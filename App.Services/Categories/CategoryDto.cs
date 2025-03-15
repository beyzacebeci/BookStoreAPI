namespace App.Services.Categories;

public record CategoryDto
{
    public int Id { get; init; }
    public string Name { get; init; } = default!;
}

