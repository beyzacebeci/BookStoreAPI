using App.Repositories.Books;

namespace App.Repositories.Categories
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public ICollection<Book> Books { get; set; }= new List<Book>();


    }
}
