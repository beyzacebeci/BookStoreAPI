namespace App.Services.Orders;

public record OrderDto
{
    public int Id { get; init; }
    public int Quantity { get; init; }
    public decimal TotalPrice { get; init; }
    public DateTime OrderDate { get; init; }
    public string Status { get; init; } = string.Empty;
    public List<OrderItemDto> Items { get; init; } = new();
}

