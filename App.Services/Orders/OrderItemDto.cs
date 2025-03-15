

namespace App.Services.Orders;

public class OrderItemDto
{
    public int BookId { get; set; }
    public int Quantity { get; set; }
    public string BookTitle { get; init; } = string.Empty;
    public string BookAuthor { get; init; } = string.Empty;
    public decimal UnitPrice { get; init; }
    public decimal SubTotal { get; init; }
}
