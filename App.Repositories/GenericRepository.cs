using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace App.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;
    public GenericRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();

    }
    public IQueryable<T> GetAll() => _dbSet.AsQueryable().AsNoTracking();
    public IQueryable<T> Where(Expression<Func<T, bool>> predicate) => _dbSet.Where(predicate).AsNoTracking();

    public ValueTask<T?> GetByIdAsync(int id) => _dbSet.FindAsync(id);
    public async ValueTask AddAsync(T entity) => await _dbSet.AddAsync(entity);
    public void Update(T entity) => _dbSet.Update(entity);

    public void Delete(T entity) => _dbSet.Remove(entity);




}

