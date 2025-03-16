namespace App.Services.Orders.Create;

public class CreateOrderResponseDto
{
    public int Id { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime OrderDate { get; set; }
}

