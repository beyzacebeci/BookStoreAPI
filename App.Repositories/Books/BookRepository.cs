namespace App.Repositories.Books;
using Microsoft.EntityFrameworkCore;

public class BookRepository : GenericRepository<Book>, IBookRepository
{
    private readonly AppDbContext _context;

    public BookRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Book>> GetBooksByCategory(int categoryId)
    {
        return await _context.Books
            .Where(b => b.CategoryId == categoryId)
            .Include(b => b.Category)  // Category bilgisini de getir
            .AsNoTracking()           // Performans için
            .ToListAsync();
    }
}

