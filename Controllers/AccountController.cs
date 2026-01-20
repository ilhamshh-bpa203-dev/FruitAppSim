using FruitSimulation.Models;
using FruitSimulation.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FruitSimulation.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);

            AppUser appUser = new AppUser() 
            {
                Name = registerVM.Name,
                Surname = registerVM.Surame,
                UserName = registerVM.Username,
                Email = registerVM.Email,
            };

            var result = await _userManager.CreateAsync(appUser,registerVM.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty,error.Description);
                    return View();
                }
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
            if (!ModelState.IsValid) return View();

            AppUser appUser = await _userManager.Users.FirstOrDefaultAsync(u=>u.Name == loginVM.UsernameOrEmail || u.Email == loginVM.UsernameOrEmail);
            if (appUser == null)
            {
                ModelState.AddModelError(string.Empty, "Username,Email or Password is incorrect..");
                return View(loginVM);
            }

            var result = await _signInManager.PasswordSignInAsync(appUser,loginVM.Password,false,false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Username,Email or Password is incorrect..");
                return View(loginVM);
            }


            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public async Task<IActionResult> Logout()
        {
             await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
