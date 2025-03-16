namespace App.Services.Books.Update;

public record UpdateBookRequestDto
{
    public int CategoryId { get; init; } // FK
    public string Title { get; init; } = default!;
    public string Author { get; init; } = default!;
    public string ISBN { get; init; } = default!;
    public decimal Price { get; init; }
    public int StockQuantity { get; init; }
    public int PublicationYear { get; init; }
}

