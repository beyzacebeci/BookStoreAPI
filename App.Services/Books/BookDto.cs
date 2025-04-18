﻿namespace App.Services.Books;

public record BookDto
{
    public int Id { get; init; }
    public int CategoryId { get; init;} // FK
    public string Title { get; init;} = default!;
    public string Author { get; init;} = default!;
    public string ISBN { get; init;} = default!;
    public decimal Price { get; init;}
    public int StockQuantity { get; init;}
    public int PublicationYear { get; init;}
}

