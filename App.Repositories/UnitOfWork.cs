﻿namespace App.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;

    }
    public Task<int> SaveChangesAsync() => _context.SaveChangesAsync();

}

