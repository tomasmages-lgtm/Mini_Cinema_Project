using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Mini_Cinema.Models
{
    public class Actor
    {
        public int Id { set; get; }
        [MaxLength(20)]
        [MinLength(2)]
        public string FName { set; get; }
        [MaxLength(20)]
        [MinLength(2)]
        public string LName { set; get; }
        public string Img { set; get; }

        public List<Movie>? Movies { get; set; }
    }
}
