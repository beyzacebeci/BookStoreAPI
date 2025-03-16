namespace App.Services.Books.Create;

public record CreateBookRequestDto
{
    public int CategoryId { get; init; }
    public string Title { get; init; } = default!;
    public string Author { get; init; } = default!;
    public string ISBN { get; init; } = default!;
    public decimal Price { get; init; }
    public int StockQuantity { get; init; }
    public int PublicationYear { get; init; }
}

