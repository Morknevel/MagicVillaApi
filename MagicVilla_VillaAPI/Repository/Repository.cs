using System.Linq.Expressions;
using MagicVilla.Data;
using MagicVilla.Models;
using MagicVilla.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _db;
    internal DbSet<T> dbset;
    public Repository(ApplicationDbContext db)
    {
        _db = db;
        this.dbset = db.Set<T>();
    }

    public async Task CreateAsync(T entity)
    {
        await dbset.AddAsync(entity);
        await SaveAsync();
    }
    

    public async Task<T> GetAsync(Expression<Func<T,bool>> filter = null, bool tracked = true,string? includeProperties = null)
    {
        IQueryable<T> query = dbset;
        if (!tracked)
        {
            query = query.AsNoTracking();
        }
        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (includeProperties != null)
        {
            foreach (var includeProp in includeProperties.Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries) )
            {
                query = query.Include(includeProp);
            }
        }
        return await query.FirstOrDefaultAsync();
    }

    public async Task<List<T>> GetAllAsync(Expression<Func<T,bool>>? filter = null,string? includeProperties = null)
    {
        IQueryable<T> query = dbset;

        if (filter != null)
        {
            query = query.Where(filter);
        }
        if (includeProperties != null)
        {
            foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProp);
            }
        }
        return await query.ToListAsync();
    }

    public async Task RemoveAsync(T entity)
    {
        dbset.Remove(entity);
        await SaveAsync();
    }
    
    
    public async Task SaveAsync()
    {
        await _db.SaveChangesAsync();
    }
}
