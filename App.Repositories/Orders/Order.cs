using App.Repositories.Books;

namespace App.Repositories.Orders
{
    public class Order
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public OrderStatus Status { get; set; } = OrderStatus.PENDING;
        public enum OrderStatus
        {
            PENDING,
            COMPLETED,
            CANCELLED
        }
        public ICollection<Book> Books { get; set; } = new List<Book>();

    }
}
