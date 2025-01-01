using System.Linq.Expressions;
using EventPulse.Domain.Interfaces;
using EventPulse.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventPulse.Infrastructure.Persistence;

public class Repository<T> : IRepository<T> where T : class, IBaseEntity
{
    private readonly DbSet<T> _dbSet;

    public Repository(DbContext dbContext)
    {
        var context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _dbSet = context.Set<T>();
    }

    public async Task<List<T>> GetAllAsync(bool tracking = false)
    {
        return await GetQueryable(tracking: tracking).ToListAsync();
    }

    public async Task<List<T>> GetAsync(Expression<Func<T, bool>> predicate, bool tracking = false)
    {
        return await GetQueryable(tracking: tracking).Where(predicate: predicate).ToListAsync();
    }

    public async Task<List<TResult>> GetAsync<TResult>(Expression<Func<T, bool>> predicate,
        Expression<Func<T, TResult>> selector, bool tracking = false)
    {
        return await GetQueryable(tracking: tracking).Where(predicate: predicate).Select(selector: selector)
            .ToListAsync();
    }

    public async Task<T?> GetSingleAsync(Expression<Func<T, bool>> predicate, bool tracking = false)
    {
        return await GetQueryable(tracking: tracking).SingleOrDefaultAsync(predicate: predicate);
    }

    public async Task<T?> FindByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<T?> FindAsync(params object[] keyValues)
    {
        return await _dbSet.FindAsync(keyValues);
    }

    public async Task<T> AddAsync(T entity)
    {
        var addedEntity = await _dbSet.AddAsync(entity);
        addedEntity.State = EntityState.Added;
        return addedEntity.Entity;
    }

    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await _dbSet.AddRangeAsync(entities);
    }

    public Task UpdateAsync(T entity)
    {
        var updatedEntity = _dbSet.Update(entity);
        updatedEntity.State = EntityState.Modified;
        return Task.CompletedTask;
    }

    public Task UpdateRangeAsync(IEnumerable<T> entities)
    {
        _dbSet.UpdateRange(entities);
        return Task.CompletedTask;
    }

    public Task RemoveAsync(T entity)
    {
        var removedEntity = _dbSet.Remove(entity);
        removedEntity.State = EntityState.Deleted;
        return Task.CompletedTask;
    }

    public Task RemoveRangeAsync(IEnumerable<T> entities)
    {
        _dbSet.RemoveRange(entities);
        return Task.CompletedTask;
    }

    public Task<int> CountAsync(Expression<Func<T, bool>> predicate)
    {
        return GetQueryable().CountAsync(predicate: predicate);
    }

    public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
    {
        return GetQueryable().AnyAsync(predicate: predicate);
    }

    public Task<bool> AllAsync(Expression<Func<T, bool>> predicate)
    {
        return GetQueryable().AllAsync(predicate: predicate);
    }

    private IQueryable<T> GetQueryable(bool tracking = false)
    {
        return tracking ? _dbSet : _dbSet.AsNoTracking();
    }
}