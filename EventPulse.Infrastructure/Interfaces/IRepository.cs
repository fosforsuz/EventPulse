using System.Linq.Expressions;
using EventPulse.Domain.Interfaces;

namespace EventPulse.Infrastructure.Interfaces;

public interface IRepository<T> where T : class, IBaseEntity
{
    Task<List<T>> GetAllAsync(bool tracking = false);

    Task<List<T>> GetAsync(Expression<Func<T, bool>> predicate, bool tracking = false);

    Task<List<TResult>> GetAsync<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector,
        bool tracking = false);

    Task<T?> GetSingleAsync(Expression<Func<T, bool>> predicate, bool tracking = false);

    Task<T?> FindByIdAsync(int id);

    Task<T?> FindAsync(params object[] keyValues);

    Task<T> AddAsync(T entity);

    Task AddRangeAsync(IEnumerable<T> entities);

    Task UpdateAsync(T entity);

    Task UpdateRangeAsync(IEnumerable<T> entities);

    Task RemoveAsync(T entity);

    Task RemoveRangeAsync(IEnumerable<T> entities);

    Task<int> CountAsync(Expression<Func<T, bool>> predicate);

    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

    Task<bool> AllAsync(Expression<Func<T, bool>> predicate);
}