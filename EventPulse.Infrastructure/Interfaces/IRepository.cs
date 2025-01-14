using System.Linq.Expressions;
using EventPulse.Domain.Interfaces;

namespace EventPulse.Infrastructure.Interfaces;

/// <summary>
///     Generic repository interface for managing entities that implement the IBaseEntity interface.
/// </summary>
/// <typeparam name="T">The type of entity being managed, constrained to classes implementing IBaseEntity.</typeparam>
public interface IRepository<T> where T : class, IBaseEntity
{
    /// <summary>
    ///     Retrieves all entities from the database.
    /// </summary>
    /// <param name="tracking">Specifies whether change tracking is enabled for the retrieved entities.</param>
    /// <returns>A task representing the asynchronous operation, with a list of all entities as the result.</returns>
    Task<List<T>> GetAllAsync(bool tracking = false);

    /// <summary>
    ///     Retrieves entities matching the specified predicate.
    /// </summary>
    /// <param name="predicate">The condition to filter the entities.</param>
    /// <param name="tracking">Specifies whether change tracking is enabled for the retrieved entities.</param>
    /// <returns>A task representing the asynchronous operation, with a list of matching entities as the result.</returns>
    Task<List<T>> GetAsync(Expression<Func<T, bool>> predicate, bool tracking = false);

    /// <summary>
    ///     Retrieves a subset of entities matching the specified predicate and projects them to a new form.
    /// </summary>
    /// <typeparam name="TResult">The type of the projected result.</typeparam>
    /// <param name="predicate">The condition to filter the entities.</param>
    /// <param name="selector">The selector function to project the entities to a new form.</param>
    /// <param name="skip">The number of entities to skip.</param>
    /// <param name="take">The maximum number of entities to take.</param>
    /// <param name="tracking">Specifies whether change tracking is enabled for the retrieved entities.</param>
    /// <returns>A task representing the asynchronous operation, with a list of projected entities as the result.</returns>
    Task<List<TResult>> GetAsync<TResult>(Expression<Func<T, bool>> predicate,
        Expression<Func<T, TResult>> selector, int skip, int take, bool tracking = false);

    /// <summary>
    ///     Retrieves entities matching the specified predicate and projects them to a new form.
    /// </summary>
    /// <typeparam name="TResult">The type of the projected result.</typeparam>
    /// <param name="predicate">The condition to filter the entities.</param>
    /// <param name="selector">The selector function to project the entities to a new form.</param>
    /// <param name="tracking">Specifies whether change tracking is enabled for the retrieved entities.</param>
    /// <returns>A task representing the asynchronous operation, with a list of projected entities as the result.</returns>
    Task<List<TResult>> GetAsync<TResult>(Expression<Func<T, bool>> predicate,
        Expression<Func<T, TResult>> selector, bool tracking = false);

    /// <summary>
    ///     Retrieves a single entity matching the specified predicate.
    /// </summary>
    /// <param name="predicate">The condition to filter the entity.</param>
    /// <param name="tracking">Specifies whether change tracking is enabled for the retrieved entity.</param>
    /// <returns>A task representing the asynchronous operation, with the matching entity as the result.</returns>
    Task<T?> GetSingleAsync(Expression<Func<T, bool>> predicate, bool tracking = false);

    /// <summary>
    ///     Retrieves a single entity matching the specified predicate and projects it to a new form.
    /// </summary>
    /// <typeparam name="TResult">The type of the projected result.</typeparam>
    /// <param name="predicate">The condition to filter the entity.</param>
    /// <param name="selector">The selector function to project the entity to a new form.</param>
    /// <param name="tracking">Specifies whether change tracking is enabled for the retrieved entity.</param>
    /// <returns>A task representing the asynchronous operation, with the projected entity as the result.</returns>
    Task<TResult?> GetSingleAsync<TResult>(Expression<Func<T, bool>> predicate,
        Expression<Func<T, TResult>> selector, bool tracking = false);

    /// <summary>
    ///     Finds an entity by its primary key.
    /// </summary>
    /// <param name="id">The primary key of the entity.</param>
    /// <returns>A task representing the asynchronous operation, with the entity as the result.</returns>
    Task<T?> FindByIdAsync(int id);

    /// <summary>
    ///     Finds an entity using a set of key values.
    /// </summary>
    /// <param name="keyValues">The values of the keys used to find the entity.</param>
    /// <returns>A task representing the asynchronous operation, with the entity as the result.</returns>
    Task<T?> FindAsync(params object[] keyValues);

    /// <summary>
    ///     Adds a new entity to the database.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <returns>A task representing the asynchronous operation, with the added entity as the result.</returns>
    Task<T> AddAsync(T entity);

    /// <summary>
    ///     Adds multiple entities to the database.
    /// </summary>
    /// <param name="entities">The collection of entities to add.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task AddRangeAsync(IEnumerable<T> entities);

    /// <summary>
    ///     Updates an existing entity in the database.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task UpdateAsync(T entity);

    /// <summary>
    ///     Updates multiple entities in the database.
    /// </summary>
    /// <param name="entities">The collection of entities to update.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task UpdateRangeAsync(IEnumerable<T> entities);

    /// <summary>
    ///     Removes an entity from the database.
    /// </summary>
    /// <param name="entity">The entity to remove.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task RemoveAsync(T entity);

    /// <summary>
    ///     Removes multiple entities from the database.
    /// </summary>
    /// <param name="entities">The collection of entities to remove.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task RemoveRangeAsync(IEnumerable<T> entities);

    /// <summary>
    ///     Counts the number of entities matching the specified predicate.
    /// </summary>
    /// <param name="predicate">The condition to filter the entities.</param>
    /// <returns>A task representing the asynchronous operation, with the count of matching entities as the result.</returns>
    Task<int> CountAsync(Expression<Func<T, bool>> predicate);

    /// <summary>
    ///     Checks if any entities match the specified predicate.
    /// </summary>
    /// <param name="predicate">The condition to filter the entities.</param>
    /// <returns>A task representing the asynchronous operation, with a boolean indicating if any entities match.</returns>
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

    /// <summary>
    ///     Checks if all entities match the specified predicate.
    /// </summary>
    /// <param name="predicate">The condition to filter the entities.</param>
    /// <returns>A task representing the asynchronous operation, with a boolean indicating if all entities match.</returns>
    Task<bool> AllAsync(Expression<Func<T, bool>> predicate);
}