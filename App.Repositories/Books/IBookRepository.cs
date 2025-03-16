namespace App.Repositories.Books;

public interface IBookRepository : IGenericRepository<Book>
{
    Task<List<Book>> GetBooksByCategoryAsync(int categoryId);
    Task<List<Book>> SearchByTitleAsync(string title);
}

