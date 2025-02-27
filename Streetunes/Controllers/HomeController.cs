using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Streetunes.Data;
using Streetunes.Helpers;
using Streetunes.Models;
using Streetunes.ViewModel;
using System.Diagnostics;
using System.Globalization;
using System.Net;

namespace Streetunes.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEventReposiroty _eventRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public HomeController(ILogger<HomeController> logger, IEventReposiroty eventReposiroty, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _logger = logger;
            _eventRepository = eventReposiroty;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task <IActionResult> Index()
        {
            var ipInfo = new IPInfo();
            var homeVM = new HomeViewModel();
            try
            {
                string url = "https://ipinfo.io?token=50e3576ee1795b";
                var info = new WebClient().DownloadString(url);
                ipInfo = JsonConvert.DeserializeObject<IPInfo>(info);
                RegionInfo myRI = new RegionInfo(ipInfo.Country);
                ipInfo.Country = myRI.EnglishName;
                homeVM.City = ipInfo.City;
                homeVM.Plz = ipInfo.Postal;

                if (homeVM.City != null)
                {
                    homeVM.Events = await _eventRepository.GetEventByPLZ(homeVM.Plz);

                }
                else
                {
                    homeVM.Events = null;
                }
                return View(homeVM);
            }
            catch (Exception ex)
            {
                homeVM.Events = null;
            }
            return View(homeVM);
        }

        [HttpPost]
        public async Task<IActionResult> Index(HomeViewModel homeVM)
        {
            var createVM = homeVM.Register;
            if (!ModelState.IsValid) return View(homeVM);

            var user = await _userManager.FindByEmailAsync(createVM.Email);
            if (user != null)
            {
                ModelState.AddModelError("Register.Email", "This email address is already in use");
                return View(homeVM);
            }

           /* var userLocation = await _locationService.GetCityByZipCode(createVM.ZipCode ?? 0);

            if (userLocation == null)
            {
                ModelState.AddModelError("Register.ZipCode", "Could not find zip code!");
                return View(homeVM);
            } */

            var newUser = new AppUser
            {
                UserName = createVM.UserName,
                Email = createVM.Email,
               
            };

            var newUserResponse = await _userManager.CreateAsync(newUser, createVM.Password);

            if (newUserResponse.Succeeded)
            {
                await _signInManager.SignInAsync(newUser, isPersistent: false);
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
