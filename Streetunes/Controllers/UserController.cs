using Microsoft.AspNetCore.Mvc;
using Streetunes.Interfaces;

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
            return View(userDetail);

        }
    }
}
