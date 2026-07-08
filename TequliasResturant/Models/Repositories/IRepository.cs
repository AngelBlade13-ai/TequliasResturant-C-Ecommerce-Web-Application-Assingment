using System.Linq.Expressions;

namespace TequliasResturant.Models.Repositories;

public interface IRepository<T> where T : class
{
    IQueryable<T> Query();

    Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);

    Task<T?> GetByIdAsync(int id);

    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

    Task<IEnumerable<T>> GetAllByIdAsync<TKey>(TKey id, string propertyName, params Expression<Func<T, object>>[] includes);

    Task AddAsync(T entity);

    void Update(T entity);

    void Delete(T entity);

    Task SaveAsync();
}
