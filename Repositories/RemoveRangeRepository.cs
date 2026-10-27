namespace Mini_Cinema.Repositories
{
    public class RemoveRangeRepository<T>:Repository<T> where T:class
    {
        public void RemoveRange(IEnumerable<T> entities) {
            _MyContext.RemoveRange(entities);    
        }
    }
}
