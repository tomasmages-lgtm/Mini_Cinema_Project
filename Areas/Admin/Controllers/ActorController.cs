using Microsoft.AspNetCore.Mvc;
using Mini_Cinema.DataAccess;
using Mini_Cinema.Models;
using Mini_Cinema.ViewModel.Admin;
using Mini_Cinema.Repositories;

namespace Mini_Cinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ActorController : Controller
    {
        //ApplicationDbContext _MyContext = new ApplicationDbContext();
        IRepository<Actor> ActorRepository;

        public ActorController(IRepository<Actor> actorRepository)
        {
            ActorRepository = actorRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create() {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Actor actor,IFormFile? ActualFile) {
            ModelState.Remove("Img");

            if (!ModelState.IsValid)
            {
                return View(new Actor());
            }

            if (ActualFile != null && ActualFile.Length > 0) {
                var ImageName = Guid.NewGuid().ToString() + "-" + ActualFile.FileName;
                var ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Mini _Cinema_Img\\Actors_Images", ImageName);
                actor.Img = ImageName;
                using (var stream = System.IO.File.Create(ImagePath))
                {
                    ActualFile.CopyTo(stream);
                }
                //_MyContext.Actors.Add(actor);
                await ActorRepository.AddAsync(actor);
                //_MyContext.SaveChanges();
                await ActorRepository.CommitAsync();
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> Update(int Id) {
            var Actor = await ActorRepository.GetOneAsync(a => a.Id == Id);//
            return View(Actor);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Actor actor,IFormFile ActualFile)
        {
            var OldActor =await ActorRepository.GetOneAsync(a => a.Id == actor.Id);
            OldActor.FName = actor.FName;
            OldActor.LName = actor.LName;
            if (ActualFile != null && ActualFile.Length > 0) {
                var OldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Mini _Cinema_Img\\Actors_Images", OldActor.Img);
                if (System.IO.File.Exists(OldPath))
                {
                    System.IO.File.Delete(OldPath);
                }
                var ImageName = Guid.NewGuid().ToString() + "-" + ActualFile.FileName;
                var ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Mini _Cinema_Img\\Actors_Images", ImageName);
                using (var stream = System.IO.File.Create(ImagePath))
                {
                    ActualFile.CopyTo(stream);
                }
                OldActor.Img = ImageName;
            }
            //_MyContext.SaveChanges();
            await ActorRepository.CommitAsync();
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Delete(int Id) {
            var Actor = await ActorRepository.GetOneAsync(a => a.Id == Id);//
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Mini _Cinema_Img\\Actors_Images", Actor.Img);
            if (System.IO.File.Exists(path)) {
                System.IO.File.Delete(path);
            }
            //_MyContext.Actors.Remove(Actor);
            ActorRepository.Remove(Actor);
            //_MyContext.SaveChanges();
            await ActorRepository.CommitAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
