namespace Mini_Cinema.Repositories
{
    public class ExtentionRepository<T>:Repository<T> where T:class
    {
        public void RemoveRange(IEnumerable<T> entities) {
            _MyContext.RemoveRange(entities);    
        }
    }
}
