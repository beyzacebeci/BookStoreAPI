using App.Repositories.Books;
using App.Repositories.Orders;

namespace App.Repositories.OrderItems;

public class OrderItem : ISoftDelete
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int BookId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public bool IsDeleted { get; set; }

    public Order Order { get; set; } = default!;
    public Book Book { get; set; } = default!;
}

