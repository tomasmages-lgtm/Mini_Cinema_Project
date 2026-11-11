using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Win32;
using Mini_Cinema.ViewModel.Identity;
using System.Collections.Specialized;

namespace Mini_Cinema.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _UserManager;
        // Regist for Dependency Inversion Principle
        private readonly SignInManager<ApplicationUser> _SignInManager;

        private readonly IEmailSender _EmailSender;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailSender emailSender)
        {
            _UserManager = userManager;
            _SignInManager = signInManager;
            _EmailSender = emailSender;
        }
        // in program cs  builder.services.addIdentity<ApplicationUser,IdentityRole> ==> inject


        [HttpGet]
        public IActionResult Register() {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM) {
            if (!ModelState.IsValid) {
                return View(registerVM);
            }

            ApplicationUser User = new ApplicationUser() // Insert Object User from Application User and set all data comming from the form  
            {
                Name=registerVM.Name,
                Address=registerVM.Address,
                UserName=registerVM.UserName,
                Email=registerVM.Email
            };
            var Result=await _UserManager.CreateAsync(User, registerVM.Password);  /* Has two overloads with user and pass  2- with user only and return Identity Result*/
            if (!Result.Succeeded) {
                foreach (var error in Result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(registerVM);
            }
            var Token =await _UserManager.GenerateEmailConfirmationTokenAsync(User);
            var Link = Url.Action(nameof(ConfirmEmail), "Account", new { area = "Identity" ,Id=User.Id,Token},Request.Scheme);
            await _EmailSender.SendEmailAsync(registerVM.Email,"Lumiere Cinema Confiemation email",$"<h1> Click <a href='{Link}'> here </a> to confirm your email </h1>");
            return RedirectToAction("Login");
        }


        public async Task<IActionResult> ConfirmEmail(string Id,string token) {
            var User =await _UserManager.FindByIdAsync(Id);
            var Result=await _UserManager.ConfirmEmailAsync(User,token);
            if (!Result.Succeeded) {
                return RedirectToAction(nameof(Login));
            }
            return RedirectToAction(nameof(Login));
        }


        [HttpGet]
        public IActionResult Login() {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM) {
            var RegisteredUser = await _UserManager.FindByEmailAsync(loginVM.UserNameOrEmail) ?? await _UserManager.FindByNameAsync(loginVM.UserNameOrEmail);
            /* for searching user becaue i pass this user in SignInManager.passwordSignAsync(user,pass) to check the pass written in form and the hash password in database */
            if (RegisteredUser == null) {
                ModelState.AddModelError("", "Invalid Cradentials");
                return View(); 
            }
            // overload in SignInPasswordAsync (user,password,boolean : (persistent) , boolean : (locout on failure)) return SignInResult 
            var Result =await _SignInManager.PasswordSignInAsync(RegisteredUser, loginVM.Password,loginVM.RememberMe,true);
            if (!Result.Succeeded) {
                if (Result.IsLockedOut)
                {
                    ModelState.AddModelError("", "To many Attempts");
                }
                else {
                    ModelState.AddModelError("", "Invalid Cradential");
                }
                return View(loginVM);
            }
            return RedirectToAction("Index", "Home", new { area = "Customer" }); 
        }
    }
}


