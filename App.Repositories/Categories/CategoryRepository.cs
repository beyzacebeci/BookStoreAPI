namespace App.Repositories.Categories;

public class CategoryRepository :GenericRepository<Category> ,ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }

