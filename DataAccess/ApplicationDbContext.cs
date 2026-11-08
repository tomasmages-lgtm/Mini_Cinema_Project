using Microsoft.EntityFrameworkCore;
using Mini_Cinema.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Mini_Cinema.DataAccess
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options){
            }
        public DbSet<Actor> Actors { set; get; }
        public DbSet<Category> categories { set; get; }
        public DbSet<Cinema> cinemas { set; get; }
        public DbSet<Movie> Movies { set; get; }

        public DbSet<MovieActors> MovieActors { set; get; }

        public DbSet<MovieSubImages> MovieSubImages { set; get; }

    }
}

