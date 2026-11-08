using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mini_Cinema.ViewModel.Identity;
using Mini_Cinema.ViewModel.Identity;

namespace Mini_Cinema.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _UserManager;

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            _UserManager = userManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVM);
            }
            ApplicationUser user = new ApplicationUser()
            {
                Name = registerVM.Name,
                Address = registerVM.Address,
                UserName = registerVM.UserName,
                Email = registerVM.Email
            };
            var Result = await _UserManager.CreateAsync(user, registerVM.Password);
            if (!Result.Succeeded)
            {
                foreach (var item in Result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View(registerVM);
            }
            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

    }
}


