using System.Linq.Expressions;
using Core.Entities;

namespace Core.Repository;

public interface IEntityRepository<T> where T : class, IEntity, new()
{
    Task<T?> GetAsync(Expression<Func<T, bool>> filter);
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);

}