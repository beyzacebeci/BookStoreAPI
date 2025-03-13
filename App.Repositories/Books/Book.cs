using App.Repositories.Categories;
using App.Repositories.Orders;

namespace App.Repositories.Books
{
    public class Book
    {
        public int Id { get; set; }
        public int CategoryId { get; set; } // FK
        public string Title { get; set; } = default!;
        public string Author { get; set; } = default!;
        public string ISBN { get; set; } = default!;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int PublicationYear { get; set; }
        public Category Category { get; set; } = null!;
        public ICollection<Order> Orders { get; set; } = new List<Order>();


    }
}
