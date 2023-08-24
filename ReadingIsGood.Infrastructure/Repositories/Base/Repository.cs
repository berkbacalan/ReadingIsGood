using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ReadingIsGood.Domain.Entities.Base;
using ReadingIsGood.Domain.Repositories.Base;
using ReadingIsGood.Infrastructure.Data;

namespace ReadingIsGood.Infrastructure.Repositories.Base;

public class Repository<T> : IRepository<T> where T : Entity
{
    protected readonly AppDbContext _dbContext;
    public Repository(AppDbContext context)
    {
        _dbContext = context;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbContext.Set<T>().Where(predicate).ToListAsync();
    }

    public Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string? includeString = null,
        bool disableTracking = true)
    {
        throw new NotImplementedException();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    public async Task<T> AddAsync(T entity)
    {
        _dbContext.Set<T>().Add(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        _dbContext.Set<T>().Update(entity);
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}