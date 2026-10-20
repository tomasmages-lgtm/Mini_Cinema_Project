using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Mini_Cinema.Models
{
    public class Category
    {
        public int Id { set; get; }

        public string Name { set; get; }
        public List<Movie> Movies { set; get; }
    }
}
