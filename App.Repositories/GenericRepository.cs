using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace App.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class, ISoftDelete
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;
    public GenericRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();

    }
    public IQueryable<T> GetAll()
    {
        return _dbSet.AsQueryable()
            .Where(x => !x.IsDeleted)
            .AsNoTracking();
    }
    public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
    {
        return _dbSet.Where(predicate)
            .Where(x => !x.IsDeleted)
            .AsNoTracking();
    }

    public async ValueTask<T?> GetByIdAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);

        if (entity == null || entity.IsDeleted)
            return null;

        return entity;
    }
    public async ValueTask AddAsync(T entity) => await _dbSet.AddAsync(entity);
    public void Update(T entity) => _dbSet.Update(entity);
}

