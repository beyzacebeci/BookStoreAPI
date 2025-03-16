using App.Services.Orders.CreateOrderItem;

namespace App.Services.Orders.Create;

public class CreateOrderRequestDto
{
    public List<CreateOrderItemRequestDto> OrderItems { get; set; } = new();
}

