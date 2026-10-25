namespace Mini_Cinema.Repositories
{
    public class MovieActorsRemoveRepository:Repository<MovieActors>
    {
        public void RemoveRange(IEnumerable<MovieActors> movieActors) {
            _MyContext.RemoveRange(movieActors);
        }
    }
}
