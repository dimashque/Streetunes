using Microsoft.AspNetCore.Mvc;
using Streetunes.Interfaces;
using Streetunes.ViewModel;

namespace Streetunes.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task <IActionResult> Detail(string id)
        {
            var userDetail = await _userRepository.GetById(id);

            if (userDetail == null) return RedirectToAction("Index", "Home");

            var userDetailVM = new UserDetailViewModel
            {
                Id = id,
                UserName = userDetail.UserName,
                ProfileImageUrl = userDetail.ProfileImageUrl,
                City = userDetail.City,
                State = userDetail.State,
                CreatedEvents = userDetail.CreatedEvents,
                Events = userDetail.Events,
            };
            return View(userDetailVM);

        }
    }
}
