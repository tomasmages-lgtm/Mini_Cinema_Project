using Mini_Cinema.Models;

namespace Mini_Cinema.ViewModel.Customer
{
    public class FilterVM
    {
        public int MovieId { set; get; } = default;
        public int CategoryId { set; get; } = default;

        public IEnumerable<Movie> Movies { set; get; }
        public IEnumerable<Category> Categories { set; get; }

        public int Page { set; get; } = 1;
    }
}
