namespace App.Services.Orders;

public class CreateOrderRequestDto
{
    public List<CreateOrderItemDto> OrderItems { get; set; } = new();
}

