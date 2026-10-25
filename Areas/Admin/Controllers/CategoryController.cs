using Microsoft.AspNetCore.Mvc;
using Mini_Cinema.DataAccess;
using Mini_Cinema.Models;
using Mini_Cinema.ViewModel.Admin;
using Mini_Cinema.Repositories;

namespace Mini_Cinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        //ApplicationDbContext _MyContext = new ApplicationDbContext();
        Repository<Category> CategoryRepository = new Repository<Category>();
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
        public async Task<IActionResult> Create(string Name) {
            Category category = new Category();
            category.Name = Name;
            //_MyContext.categories.Add(category);
            await CategoryRepository.AddAsync(category);
            //_MyContext.SaveChanges();
            await CategoryRepository.CommitAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> Update(int Id) {
            //var Category = _MyContext.categories.FirstOrDefault(c => c.Id == Id);
            var Category =await CategoryRepository.GetOneAsync(c => c.Id == Id);
            return View(Category);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Category category) {
            //var OldCategory = _MyContext.categories.FirstOrDefault(c => c.Id == category.Id);
            var OldCategory =await CategoryRepository.GetOneAsync(c => c.Id == category.Id);
            OldCategory.Name = category.Name;
            //_MyContext.SaveChanges();
            await CategoryRepository.CommitAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Delete(int Id) {
            //var Category = _MyContext.categories.FirstOrDefault(c => c.Id == Id);
            var Category= await CategoryRepository.GetOneAsync(c => c.Id == Id);
            //_MyContext.categories.Remove(Category);
            CategoryRepository.Remove(Category);
            //_MyContext.SaveChanges();
            await CategoryRepository.CommitAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
