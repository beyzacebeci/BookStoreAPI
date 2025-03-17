using App.Repositories.Books;

namespace App.Repositories.Categories;

public class Category : ISoftDelete
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public bool IsDeleted { get; set; }
    public ICollection<Book> Books { get; set; } = new List<Book>();


}

