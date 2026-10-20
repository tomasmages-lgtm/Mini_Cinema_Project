using Mini_Cinema.Models;

namespace Mini_Cinema.ViewModel.Admin
{
    public class AdminIndexData
    {
        public IEnumerable<Movie> Movies { set; get; }
        public IEnumerable<Category> Categories { set; get; }
        public IEnumerable<Actor> Actors { set; get; }
    }
}
