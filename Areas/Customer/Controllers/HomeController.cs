using Microsoft.AspNetCore.Mvc;
using Mini_Cinema.DataAccess;
using Mini_Cinema.Models;
using System.Diagnostics;
using Mini_Cinema.ViewModel.Customer;
using Mini_Cinema.Repositories;

namespace Mini_Cinema.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        Repository<Category> CategoryRepository = new Repository<Category>();
        Repository<Movie> MovieRepository = new Repository<Movie>();

        //ApplicationDbContext _MyContext = new ApplicationDbContext();
        public async Task<IActionResult> Index(FilterVM filter)
        {
            var Movies =await MovieRepository.GetAllAsync();
            if (filter.MovieId != default) {
            Movies = MovieRepository.Query(m => m.Id == filter.MovieId);
            }
            if (filter.CategoryId !=default) {
                Movies = MovieRepository.Query(m => m.CategoryId == filter.CategoryId);
            }

            int SizePages = 4;
            ViewBag.TotalPages =(int) Math.Ceiling(Movies.Count() /(decimal) SizePages);
            Movies = Movies.Skip((filter.Page - 1) * SizePages).Take(SizePages);

            ViewBag.Movies =await MovieRepository.GetAllAsync();
            return View(new FilterVM
            {
                Movies = Movies,
                Categories =await CategoryRepository.GetAllAsync()
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
