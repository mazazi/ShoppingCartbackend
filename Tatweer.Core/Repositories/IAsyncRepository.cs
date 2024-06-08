using System.Linq.Expressions;
using Tatweer.Core.Common;

namespace Tatweer.Core.Repositories;

public interface IAsyncRepository<T> where T: class
{
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
    Task<T> GetByIdAsync(int id);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    IQueryable<T> Find(Expression<Func<T, bool>> predicate);
    
}