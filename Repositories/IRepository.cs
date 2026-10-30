using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Mini_Cinema.Repositories
{
    public interface IRepository<T> where T :class
    {
          Task<EntityEntry<T>> AddAsync(T entity);

         void Remove(T entity);

          Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>>? filter = null,
            Expression<Func<T, object>>?[] includes = null
            );

          Task<T> GetOneAsync(
        Expression<Func<T, bool>>? filter = null,
        Expression<Func<T, object>>?[] includes = null
            );

         IQueryable<T> Query(
            Expression<Func<T, bool>>? filter = null,
            Expression<Func<T, object>>?[] includes = null
            );

          Task<int> CommitAsync();
    }
}
