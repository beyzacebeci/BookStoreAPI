using App.Repositories.Authors;
using App.Repositories.Categories;
using App.Repositories.Orders;

namespace App.Repositories.Books;

public class Book : ISoftDelete
{
    public int Id { get; set; }
    public int AuthorId { get; set; } = default!;
    public int CategoryId { get; set; }
    public string Title { get; set; } = default!;
    public string ISBN { get; set; } = default!;
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public int PublicationYear { get; set; }
    public bool IsDeleted { get; set; }

    public Category Category { get; set; } = default!;
    public Author Author { get; set; } = default!;
    public ICollection<Order> Orders { get; set; } = new List<Order>();

}

