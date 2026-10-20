using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Mini_Cinema.Models
{
    public class Actor
    {
        public int Id { set; get; }
        public string FName { set; get; }
        public string LName { set; get; }
        public string Img { set; get; }

        public List<Movie> Movies { get; set; }
    }
}
