using Microsoft.EntityFrameworkCore;
using Mini_Cinema.Models;

namespace Mini_Cinema.DataAccess
{
    public class ApplicationDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=.;Initial Catalog=Mini_Cinema;" +
            "Trusted_Connection=true;" +
            "TrustServerCertificate=true;Encrypt=true");
        }
        public DbSet<Actor> Actors { set; get; }
        public DbSet<Category> categories { set; get; }
        public DbSet<Cinema> cinemas { set; get; }
        public DbSet<Movie> Movies { set; get; }

        public DbSet<MovieActors> MovieActors { set; get; }

        public DbSet<MovieSubImages> MovieSubImages { set; get; }

    }
}

