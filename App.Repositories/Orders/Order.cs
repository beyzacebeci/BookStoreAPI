using App.Repositories.OrderItems;

namespace App.Repositories.Orders;

public class Order : ISoftDelete
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public decimal TotalPrice { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.PENDING;
    public bool IsDeleted { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public enum OrderStatus
    {
        PENDING,
        COMPLETED,
        CANCELLED
    }
}

