using Microsoft.AspNetCore.Mvc;

namespace Streetunes.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventReposiroty _eventReposiroty;

        public EventController(IEventReposiroty eventReposiroty)
        {
            _eventReposiroty = eventReposiroty;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
