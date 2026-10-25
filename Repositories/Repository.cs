using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Mini_Cinema.DataAccess;
using System.Linq.Expressions;

namespace Mini_Cinema.Repositories
{
    public class Repository<T>where T :class
    {
        protected ApplicationDbContext _MyContext = new ApplicationDbContext();
        private DbSet<T> _DbSet;
        public Repository()
        {
            _DbSet = _MyContext.Set<T>();
        }

        public async Task<EntityEntry<T>> AddAsync(T entity)
        {
            return await _DbSet.AddAsync(entity);
        }

        public void Remove(T entity)
        {
            _DbSet.Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>>? filter = null,
            Expression<Func<T, object>>?[] includes = null
            ) {
            var Entities = Query(filter, includes);
            return await Entities.ToListAsync();
        }

        public async Task<T> GetOneAsync(
        Expression<Func<T, bool>>? filter = null,
        Expression<Func<T, object>>?[] includes = null
            )
        {
            var Entities = Query(filter, includes);
            return await Entities.FirstOrDefaultAsync();
        }

        public  IQueryable<T>Query(
            Expression<Func<T, bool>>? filter = null,
            Expression<Func<T, object>>?[] includes=null
            ){
            var Entities =  _DbSet.AsQueryable();
            if (filter != null) {
                Entities = Entities.Where(filter);
            }
            if (includes != null) {
                foreach (var include in includes)
                {
                    Entities = Entities.Include(include);
                }
            }
            return Entities;
        }

        public async Task<int> CommitAsync()
        {
            try
            {
                return await _MyContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}
