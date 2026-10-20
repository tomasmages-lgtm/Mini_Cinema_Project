using Microsoft.AspNetCore.Mvc;
using Mini_Cinema.DataAccess;
using Mini_Cinema.Models;
using System.Diagnostics;
using Mini_Cinema.ViewModel.Customer;

namespace Mini_Cinema.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        ApplicationDbContext _MyContext = new ApplicationDbContext();
        public IActionResult Index(FilterVM filter)
        {
            var Movies = _MyContext.Movies.AsQueryable();
            if (filter.MovieId != default) {
            Movies = Movies.Where(m => m.Id == filter.MovieId);
            }
            if (filter.CategoryId !=default) {
                Movies = Movies.Where(m => m.CategoryId == filter.CategoryId);
            }

            int SizePages = 4;
            ViewBag.TotalPages =(int) Math.Ceiling(Movies.Count() /(decimal) SizePages);
            Movies = Movies.Skip((filter.Page - 1) * SizePages).Take(SizePages);

            ViewBag.Movies = _MyContext.Movies.ToList();
            return View(new FilterVM
            {
                Movies =Movies,
                Categories=_MyContext.categories
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
