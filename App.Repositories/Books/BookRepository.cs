namespace App.Repositories.Books;
using Microsoft.EntityFrameworkCore;

public class BookRepository : GenericRepository<Book>, IBookRepository
{
    private readonly AppDbContext _context;

    public BookRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Book>> GetBooksByCategoryAsync(int categoryId)
    {
        return await _context.Books
            .Where(b => b.CategoryId == categoryId && b.IsDeleted == false)
            .Include(b => b.Category)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<Book>> SearchByTitleAsync(string title)
    {
        return await _context.Books
            .Where(b => b.Title.ToLower().Contains(title.ToLower()) && b.IsDeleted == false)
            .Include(b => b.Category)
            .AsNoTracking()
            .ToListAsync();
    }
}

