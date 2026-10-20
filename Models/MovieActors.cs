using Microsoft.EntityFrameworkCore;

namespace Mini_Cinema.Models
{
    [PrimaryKey(nameof(MovieId),nameof(ActorId))]
    public class MovieActors
    {
        public int MovieId { set; get; }
        public int ActorId { set; get; }

        public Movie Movie { set; get; }

        public Actor Actor { set; get; }
    }
}
