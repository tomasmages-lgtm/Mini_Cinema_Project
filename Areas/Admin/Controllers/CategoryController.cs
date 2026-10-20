using Microsoft.AspNetCore.Mvc;
using Mini_Cinema.DataAccess;
using Mini_Cinema.Models;
using Mini_Cinema.ViewModel.Admin;

namespace Mini_Cinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        ApplicationDbContext _MyContext = new ApplicationDbContext();
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(string Name) {
            Category category = new Category();
            category.Name = Name;
            _MyContext.categories.Add(category);
            _MyContext.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Update(int Id) {
            var Category = _MyContext.categories.FirstOrDefault(c => c.Id == Id);
            return View(Category);
        }
        [HttpPost]
        public IActionResult Update(Category category) {
            Category OldCategory = _MyContext.categories.FirstOrDefault(c => c.Id == category.Id);
            OldCategory.Name = category.Name;
            _MyContext.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Delete(int Id) {
            var Category = _MyContext.categories.FirstOrDefault(c => c.Id == Id);
            _MyContext.categories.Remove(Category);
            _MyContext.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
