namespace Mini_Cinema.Repositories
{
    public class MovieSubImagesRemoveRepository:Repository<MovieSubImages>
    {
        public void RemoveRange(IEnumerable<MovieSubImages> subimages) {
            _MyContext.RemoveRange(subimages);    
        }
    }
}
