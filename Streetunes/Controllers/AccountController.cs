using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Streetunes.Data;
using Streetunes.Models;
using Streetunes.ViewModel;

namespace Streetunes.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDBcontext _context;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationDBcontext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);

            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);

            if (user != null)
            {
                // user is found check password
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (passwordCheck)
                {
                    // password correct signIn
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }


                }
                // password Wrong!!
                TempData["Error"] = "Wrong Credential, try again ";
                return View(loginVM);
            }
            //User Not found !!
            TempData["Error"] = "Wrong Credential, try again ";
            return View(loginVM);
        }

        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }
        [HttpPost]

        public async Task<IActionResult> Register(RegisterViewModel RegisterVM)
        {
            if (!ModelState.IsValid) return View(RegisterVM);

            var user = await _userManager.FindByEmailAsync(RegisterVM.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "This User already exists";
                return View(RegisterVM);
            }

            var newUser = new AppUser()
            {
                Email = RegisterVM.EmailAddress,
                UserName = RegisterVM.UserName,


            };
            var newUserResponse = await _userManager.CreateAsync(newUser, RegisterVM.Password);
            if (newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            }
            return RedirectToAction("Index", "Event");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
