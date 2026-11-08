using Microsoft.AspNetCore.Mvc;
using Mini_Cinema.ViewModel.Identity;

namespace Mini_Cinema.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterVM registerVM)
        {
            return View();
        }
    }
}
