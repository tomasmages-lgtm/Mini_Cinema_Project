using Mini_Cinema.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Mini_Cinema.DataAccess;
using Mini_Cinema.DataAccess;
using Mini_Cinema.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Mini_Cinema.ViewModel.Customer;
using Mini_Cinema.ViewModel.Admin;
using Mini_Cinema.Repositories;

namespace Mini_Cinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        Repository<Category> CategoryRepository = new Repository<Category>();
        Repository<Actor> ActorRepository = new Repository<Actor>();
        Repository<Cinema> CinemaRepository = new Repository<Cinema>();
        Repository<Movie> MovieRepository = new Repository<Movie>();
        //ApplicationDbContext _MyContext = new ApplicationDbContext();
        public async Task<IActionResult> Index()
        {
            return View(new AdminIndexData
            {
                Movies =await MovieRepository.GetAllAsync(includes: [m => m.Cinema]),
                Categories = await CategoryRepository.GetAllAsync(),
                Actors =await ActorRepository.GetAllAsync()
            });
        }

        public async Task<IActionResult> GetAllMovies() {
            return View(await MovieRepository.GetAllAsync(includes: [m=>m.Category,m=>m.Cinema]));
        }

        public async Task<IActionResult> GetAllCategories()
        {
            return View(await CategoryRepository.GetAllAsync());
        }

        public async Task<IActionResult> GetAllCinemas()
        {
            return View(await CinemaRepository.GetAllAsync());
        }

        public async Task<IActionResult> GetAllActors() {
            return View(await ActorRepository.GetAllAsync(includes:[a => a.Movies]));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        
    }
}
