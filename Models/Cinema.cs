namespace Mini_Cinema.Models
{
    public class Cinema
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Address { get; set; }
        public string? Img { set; get; }
        public List<Movie> Movies { set; get; }

    }
}
