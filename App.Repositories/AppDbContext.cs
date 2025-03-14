using App.Repositories.Books;
using App.Repositories.Categories;
using App.Repositories.Orders;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace App.Repositories;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Book> Books { get; set; } = default!;
    public DbSet<Category> Categories { get; set; } = default!;
    public DbSet<Order> Orders { get; set; } = default!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}

