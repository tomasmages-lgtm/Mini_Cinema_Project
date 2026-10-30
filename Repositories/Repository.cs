using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Mini_Cinema.DataAccess;
using System.Linq.Expressions;

namespace Mini_Cinema.Repositories
{
    public class Repository <T>:IRepository<T> where T :class
    {
        protected ApplicationDbContext _MyContext;//= new ApplicationDbContext();
        private DbSet<T> _DbSet;
        public Repository(ApplicationDbContext myContext)
        {
            _MyContext = myContext;
            _DbSet = _MyContext.Set<T>(); //  شوف ال جينيرك بتاعك نوعه ايه وخليليهولى في  dbset
        }

        public async Task<EntityEntry<T>> AddAsync(T entity) // Done
        {
            return await _DbSet.AddAsync(entity);
        }

        public void Remove(T entity) // Done
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
            var Entities =  _DbSet.AsQueryable(); // GetAll ==> _MyContex.Table<T>
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
