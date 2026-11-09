using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Mini_Cinema.Utility;
using Mini_Cinema.ViewModel.Identity;
using Mini_Cinema.ViewModel.Identity;

namespace Mini_Cinema.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly SignInManager<ApplicationUser> _SignInManager;

        private readonly IEmailSender _EmailSender;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailSender emailSender)
        {
            _UserManager = userManager;
            _SignInManager = signInManager;
            _EmailSender = emailSender;
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
            var Token = await _UserManager.GenerateEmailConfirmationTokenAsync(user);
            var link = Url.Action(nameof(ConfirmEmail), "Account", new { area = "Identity" , userID =user.Id, token=Token });
            await _EmailSender.SendEmailAsync(
                registerVM.Email,
                "Cinema Confirmation Email",
                "<h1> please click <a href='{link}'> here </a> to confirm your Email "
                );
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> ConfirmEmail( string userID,string token ) {
            var User =await _UserManager.FindByIdAsync(userID);
            var Result= await _UserManager.ConfirmEmailAsync(User,token);
            if (!Result.Succeeded) {
                return RedirectToAction(nameof(Login));
            }
            return RedirectToAction(nameof(Login));
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
            } else if (Result.IsNotAllowed) {
                ModelState.AddModelError("", "Plz Confirm you Email!!");
            }
            return RedirectToAction("Index","Home",new {area="Customer" });
        }
    }
}


