using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mini_Cinema.DataAccess;
using Mini_Cinema.Models;
using Mini_Cinema.ViewModel.Admin;
using Mini_Cinema.Repositories;

namespace Mini_Cinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MovieController : Controller
    {
        //ApplicationDbContext _MyContext = new ApplicationDbContext();
        IRepository<Movie> MovieRepository;
        IRepository<Category> CategoryRepository;
        IRepository<Cinema> CinemaRepository;
        IRepository<Actor> ActorRepository;
        IExtentionRepository<MovieSubImages> MovieSubImageRepository;
        IExtentionRepository<MovieActors> MovieActorRepository;

        public MovieController(IRepository<Movie> movieRepository, IRepository<Category> categoryRepository, IRepository<Cinema> cinemaRepository, IRepository<Actor> actorRepository, IExtentionRepository<MovieSubImages> movieSubImageRepository, IExtentionRepository<MovieActors> movieActorRepository)
        {
            MovieRepository = movieRepository;
            CategoryRepository = categoryRepository;
            CinemaRepository = cinemaRepository;
            ActorRepository = actorRepository;
            MovieSubImageRepository = movieSubImageRepository;
            MovieActorRepository = movieActorRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View(new FillMovieFormVM
            {
                Categories = await CategoryRepository.GetAllAsync(),
                Cinemas = await CinemaRepository.GetAllAsync(),
                Actors = await ActorRepository.GetAllAsync()
            });
        }
        [HttpPost]
        public async Task<IActionResult> Create(MovieDataVM moviedata)
        {
            if (moviedata.ActualFile != null && moviedata.ActualFile.Length > 0)
            {
                var ImageName = Guid.NewGuid().ToString() + "-" + moviedata.ActualFile.FileName;
                var ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Mini _Cinema_Img\\Movies_Images", ImageName);
                using (var stream = System.IO.File.Create(ImagePath))
                {
                    moviedata.ActualFile.CopyTo(stream);
                }
                moviedata.Movie.MainImg = ImageName;
            }
            //var CreatedMovie = _MyContext.Movies.Add(moviedata.Movie);
            var CreatedMovie = await MovieRepository.AddAsync(moviedata.Movie);
            //_MyContext.SaveChanges();
            await MovieRepository.CommitAsync();

            //SubImages
            //Adding in DataBase in Many TYo Many Table 
            // Adding in WWW Root all SubImages
            if (moviedata.ActualFiles != null && moviedata.ActualFiles.Count > 0)
            {
                foreach (var image in moviedata.ActualFiles)
                {
                    var ImageName = Guid.NewGuid().ToString() + "-" + image.FileName;
                    var ImagePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\Mini _Cinema_Img\\Movies_SubImages", ImageName);
                    using (var stream = System.IO.File.Create(ImagePath))
                    {
                        image.CopyTo(stream);
                    }
                    //_MyContext.MovieSubImages.Add(new MovieSubImages
                    //{
                    //    SubImage = ImageName,
                    //    MovieId = CreatedMovie.Entity.Id
                    //});

                    await MovieSubImageRepository.AddAsync(new MovieSubImages
                    {
                        SubImage = ImageName,
                        MovieId = CreatedMovie.Entity.Id
                    });
                }
                //_MyContext.SaveChanges();
                await MovieSubImageRepository.CommitAsync();
            }
            if (moviedata.ActorId != null && moviedata.ActorId.Count > 0)
            {
                foreach (var actorid in moviedata.ActorId)
                {
                    await MovieActorRepository.AddAsync(new MovieActors
                    {
                        ActorId = actorid,
                        MovieId = CreatedMovie.Entity.Id
                    });
                }
            }
            //_MyContext.SaveChanges();
            await MovieActorRepository.CommitAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> Update(int Id)
        {
            //var Movie = _MyContext.Movies.Include(m => m.Category).Include(m => m.Cinema).FirstOrDefault(m => m.Id == Id);
            var Movie =await MovieRepository.GetOneAsync(m => m.Id == Id, [m => m.Category, m => m.Cinema]);
            return View(new FillMovieFormVM
            {
                Movie = Movie,
                Categories = await CategoryRepository.GetAllAsync(),
                Cinemas = await CinemaRepository.GetAllAsync(),
                Actors = await ActorRepository.GetAllAsync(),
                MovieSubImages =await MovieSubImageRepository.GetAllAsync(s => s.MovieId == Id)
            });
        }

        [HttpPost]
        public async Task<IActionResult> Update(MovieDataVM moviedate)
        {
            //var OldMovie = _MyContext.Movies.FirstOrDefault(m => m.Id == moviedate.Movie.Id);
            var OldMovie = await MovieRepository.GetOneAsync(m => m.Id == moviedate.Movie.Id);
            if (moviedate.Movie != null)
            {
                OldMovie.Name = moviedate.Movie.Name;
                OldMovie.Description = moviedate.Movie.Description;
                OldMovie.Price = moviedate.Movie.Price;
                OldMovie.Status = moviedate.Movie.Status;
                OldMovie.CategoryId = moviedate.Movie.CategoryId;
                OldMovie.CinemaId = moviedate.Movie.CinemaId;
                if (moviedate.ActualFile != null && moviedate.ActualFile.Length > 0)
                {
                    var OldPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\Mini _Cinema_Img\\Movies_Images",OldMovie.MainImg);
                    if (System.IO.File.Exists(OldPath))
                    {
                        System.IO.File.Delete(OldPath);
                    }
                    var ImageName = Guid.NewGuid().ToString() +"-" + moviedate.ActualFile.FileName;
                    var ImagePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\Mini _Cinema_Img\\Movies_Images",ImageName);
                    using (var stream = System.IO.File.Create(ImagePath)) 
                    {
                        moviedate.ActualFile.CopyTo(stream);
                    }
                    OldMovie.MainImg = ImageName;
                }
                //_MyContext.SaveChanges();
                await MovieRepository.CommitAsync();
            }
            if (moviedate.ActualFiles != null && moviedate.ActualFiles.Count > 0)
            {
                //var OldSubImages = _MyContext.MovieSubImages.Where(s => s.MovieId == moviedate.Movie.Id);
                var OldSubImages =await MovieSubImageRepository.GetAllAsync(s => s.MovieId == moviedate.Movie.Id);
                foreach (var img in OldSubImages)
                {
                    var oldPath = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot", "Mini _Cinema_Img", "Movies_SubImages", img.SubImage);

                    if (System.IO.File.Exists(oldPath))
                        System.IO.File.Delete(oldPath);
                }
                //_MyContext.RemoveRange(OldSubImages);
                MovieSubImageRepository.RemoveRange(OldSubImages);
                await MovieSubImageRepository.CommitAsync();

                foreach (var file in moviedate.ActualFiles)
                {
                    if (file.Length > 0)
                    {
                        var fileName = Guid.NewGuid().ToString() + "-" +file.FileName;
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\Mini _Cinema_Img\\Movies_SubImages", fileName);
                        using (var stream = System.IO.File.Create(filePath))
                        {
                            file.CopyTo(stream);
                        }
                        //_MyContext.MovieSubImages.Add(new MovieSubImages
                        //{
                        //    SubImage = fileName,
                        //    MovieId = moviedate.Movie.Id
                        //});

                        await MovieSubImageRepository.AddAsync(new MovieSubImages
                        {
                            SubImage = fileName,
                            MovieId = moviedate.Movie.Id
                        });
                    }
                }
                //_MyContext.SaveChanges();
                await MovieSubImageRepository.CommitAsync();
            await MovieRepository.CommitAsync();
            }


            //var oldActors = _MyContext.MovieActors
            //    .Where(ma => ma.MovieId == OldMovie.Id);
            var oldActors= await MovieActorRepository.GetAllAsync(ma => ma.MovieId == OldMovie.Id);
            //_MyContext.MovieActors.RemoveRange(oldActors);
            MovieActorRepository.RemoveRange(oldActors);
            if (moviedate.ActorId != null)
            {
                foreach (var actorId in moviedate.ActorId)
                {
                    //_MyContext.MovieActors.Add(new MovieActors
                    //{
                    //    MovieId = OldMovie.Id,
                    //    ActorId = actorId
                    //});
                    await MovieActorRepository.AddAsync(new MovieActors
                    {
                        MovieId = OldMovie.Id,
                        ActorId = actorId
                    });

                }
            }
            //_MyContext.SaveChanges();
            await MovieActorRepository.CommitAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Delete(int Id) {
            //var Movie = _MyContext.Movies.FirstOrDefault(m => m.Id == Id);
            var Movie =await MovieRepository.GetOneAsync(m => m.Id == Id);
            var OldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Mini _Cinema_Img\\Movies_Images", Movie.MainImg);
            if (System.IO.File.Exists(OldPath)) {
                System.IO.File.Delete(OldPath);
            }
            //var OldSubs = _MyContext.MovieSubImages.Where(s => s.MovieId == Movie.Id);
            var OldSubs =await MovieSubImageRepository.GetAllAsync(s => s.MovieId == Movie.Id);
            foreach (var item in OldSubs)
            {
                var OldSubPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Mini _Cinema_Img\\Movies_SubImages", item.SubImage);
                if (System.IO.File.Exists(OldSubPath)) {
                    System.IO.File.Delete(OldSubPath);
                }
            }
            //_MyContext.Movies.Remove(Movie);
            MovieRepository.Remove(Movie);
            //_MyContext.SaveChanges();
            await MovieRepository.CommitAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}

