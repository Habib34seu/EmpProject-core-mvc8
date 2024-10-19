
using Emp.Infrastructure.Data;
using Emp.Infrastructure.Repositories.IRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Emp.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{

    private ApplicationDbContext _context;
    internal DbSet<T> dbSet;
    public Repository(ApplicationDbContext context)
    {
        _context = context;
        this.dbSet = _context.Set<T>();

    }
    public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter)
    {
        IQueryable<T> query = dbSet;
        query =  query.Where(filter);
        return await query.ToListAsync();
    }

    public async Task<T> Get(Expression<Func<T, bool>> filter)
    {
        IQueryable<T> query = dbSet;
        query = query.Where(filter);

        return await query.FirstOrDefaultAsync();
    }

    public async Task Add(T entity)
    {
       await dbSet.AddAsync(entity);
        
    }

    public async Task Remove(T entity)
    {
        dbSet.Remove(entity);
       
    }

}
