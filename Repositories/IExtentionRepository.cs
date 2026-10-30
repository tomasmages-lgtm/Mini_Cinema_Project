namespace Mini_Cinema.Repositories
{
    public interface IExtentionRepository<T>:IRepository<T> where T:class
    {
        void RemoveRange(IEnumerable<T> entities);
    }
}
