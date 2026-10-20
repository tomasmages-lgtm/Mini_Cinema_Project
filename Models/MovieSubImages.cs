using Microsoft.EntityFrameworkCore;

namespace Mini_Cinema.Models
{
    [PrimaryKey(nameof(MovieId),nameof(SubImage))]
    public class MovieSubImages
    {
        public int MovieId { set; get; }
        public string SubImage { set; get; }

        public Movie Movie { set; get; }
    }
}
