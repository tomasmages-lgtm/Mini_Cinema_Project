using Mini_Cinema.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Mini_Cinema.DataAccess;
using Mini_Cinema.DataAccess;
using Mini_Cinema.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Mini_Cinema.ViewModel.Customer;
using Mini_Cinema.ViewModel.Admin;

namespace Mini_Cinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        ApplicationDbContext _MyContext = new ApplicationDbContext();
        public IActionResult Index()
        {
            return View(new AdminIndexData
            {
                Movies = _MyContext.Movies.Include(m => m.Cinema).ToList(),
                Categories = _MyContext.categories,
                Actors = _MyContext.Actors
            });
        }

        public IActionResult GetAllMovies() {
            return View(_MyContext.Movies.Include(m=>m.Category).Include(m=>m.Cinema));
        }

        public IActionResult GetAllCategories()
        {
            return View(_MyContext.categories);
        }

        public IActionResult GetAllCinemas()
        {
            return View(_MyContext.cinemas);
        }

        public IActionResult GetAllActors() {
            return View(_MyContext.Actors.Include(a => a.Movies));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        
    }
}
