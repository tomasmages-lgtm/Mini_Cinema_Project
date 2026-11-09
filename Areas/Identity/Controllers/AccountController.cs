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
        private readonly SignInManager<ApplicationUser> _SignInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _UserManager = userManager;
            _SignInManager = signInManager;
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
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            var User = await _UserManager.FindByEmailAsync(loginVM.UserNameOrEmail) ?? await _UserManager.FindByNameAsync(loginVM.UserNameOrEmail);
            if (User == null) {
                ModelState.AddModelError("", "Invalid UserName or Password");
                return View(loginVM);
            }
            var Result= await _SignInManager.PasswordSignInAsync(User,loginVM.Password,loginVM.RememberMe,true);
            if (!Result.Succeeded) {
                if (Result.IsLockedOut) ModelState.AddModelError("", "too many attempts");
                return View(loginVM);
            }
            return RedirectToAction("Index","Home",new {area="Customer" });
        }
    }
}


