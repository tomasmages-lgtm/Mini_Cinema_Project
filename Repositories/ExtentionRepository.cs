namespace Mini_Cinema.Repositories
{
    public class ExtentionRepository<T>:Repository<T>,IExtentionRepository<T> where T:class
    {
        public ExtentionRepository(ApplicationDbContext myContext) : base(myContext)
        {
        }

        public void RemoveRange(IEnumerable<T> entities) {
            _MyContext.RemoveRange(entities);    
        }
    }
}
