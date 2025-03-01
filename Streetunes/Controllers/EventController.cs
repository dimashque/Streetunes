using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Streetunes.Models;
using Streetunes.ViewModel;

namespace Streetunes.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventReposiroty _eventReposiroty;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EventController(IEventReposiroty eventReposiroty, IHttpContextAccessor httpContextAccessor)
        {
            _eventReposiroty = eventReposiroty;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task <IActionResult> Index()

        {
            IEnumerable<Event> events = await _eventReposiroty.GetAll();    
            return View(events);
        }

        public async Task<IActionResult> Detail(int eventId)
        {
            var streetEvent = await _eventReposiroty.GetByIdAsync(eventId);
            var eventDetailVM = new EventDetailViewModel
            {
                Id = streetEvent.Id,
                Title = streetEvent.Title,
                Date = streetEvent.Date,
                Description = streetEvent.Description,
                street = streetEvent.Address.street,
                City = streetEvent.Address.street,
                Region = streetEvent.Address.Region,
                PostalCode = streetEvent.Address.PostalCode,
                OwnerName = streetEvent.Owner.UserName,
                Image = streetEvent.Image,
                EventCategory = streetEvent.EventCategory,
                FollowerCount = streetEvent.Followers.Count(),
                Comments = streetEvent.Comments

            };
            return View(eventDetailVM);
        }
        [HttpPost]
        public async Task <IActionResult> Attend (int eventId)
        {
            var streetEvent = await _eventReposiroty.GetByIdAsync(eventId);

            var curUserId = _httpContextAccessor.HttpContext.User.GetUserID();

            var curUser = await _eventReposiroty.GetCurrentUser(curUserId);


            if (streetEvent != null && curUser != null && !streetEvent.Followers.Contains(curUser)){
                streetEvent.Followers.Add(curUser);
                _eventReposiroty.Update(streetEvent);
                
            }

            return RedirectToAction("Detail", new { id = eventId });

        }
        [HttpPost]
        public async Task<IActionResult> AddComment (AddCommentViewModel addCommentVM)
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
                    
            }
            var curUserId = _httpContextAccessor.HttpContext.User.GetUserID();

            var curUser = await _eventReposiroty.GetCurrentUser(curUserId);

            if (curUser == null)
            {
                return Unauthorized();
            }

            var eventComment = new Comment
            {
                CommentText = addCommentVM.CommentText,
                CreatedDate = DateTime.Now,
                CommenterId = curUserId,
                Commenter = curUser,
                EventId = addCommentVM.EventId,

            };
            var streetEvent = await _eventReposiroty.GetByIdAsync(addCommentVM.EventId);
            streetEvent.Comments.Add(eventComment);
            _eventReposiroty.Update(streetEvent);

            return RedirectToAction("Detail", new { id = addCommentVM.EventId });

        }
    }
}
