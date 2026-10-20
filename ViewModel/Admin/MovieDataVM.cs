using Mini_Cinema.Models;

namespace Mini_Cinema.ViewModel.Admin
{
    public class MovieDataVM
    {

        public Movie Movie { set; get; }
        public IFormFile ActualFile { set; get; }

        public List<IFormFile> ActualFiles { set; get; }
        public List<int> ActorId { set; get; }
    }
}
