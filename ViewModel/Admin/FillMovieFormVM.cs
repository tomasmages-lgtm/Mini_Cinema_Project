using Mini_Cinema.Models;

namespace Mini_Cinema.ViewModel.Admin
{
    public class FillMovieFormVM
    {
        public Movie Movie { set; get; }
        public IEnumerable<Category> Categories { set; get; }
        public IEnumerable<Cinema> Cinemas { set; get; }

        public IEnumerable<Actor> Actors { set; get; }

        public IEnumerable<MovieSubImages> MovieSubImages { set; get; }
    }
}
