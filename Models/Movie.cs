namespace Mini_Cinema.Models
{
    public enum MovieState { 
    CoomingSoon=1,
    NowShowing=2
    }

    public class Movie
    {
        public int Id { set; get; }

        public string Name { set; get; }

        public string Description { set; get; }

        public decimal Price { get; set; }
        public MovieState Status { get; set; }

        public string MainImg { set; get; }
 

        public DateTime DateTime{ set; get; }

        public List<Actor> Actors { get; set; }

        public int CinemaId { set; get; }

        public int CategoryId { set; get; }

        public Cinema Cinema { set; get; }

        public Category Category { set; get; }
    }
}
