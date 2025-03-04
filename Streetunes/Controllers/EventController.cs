﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Streetunes.Interfaces;
using Streetunes.Models;
using Streetunes.ViewModel;

namespace Streetunes.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventReposiroty _eventReposiroty;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPhotoService _photoService;

        public EventController(IEventReposiroty eventReposiroty, IHttpContextAccessor httpContextAccessor, IPhotoService photoService)
        {
            _eventReposiroty = eventReposiroty;
            _httpContextAccessor = httpContextAccessor;
            _photoService = photoService;
        }

        public async Task <IActionResult> Index()

        {
            IEnumerable<Event> events = await _eventReposiroty.GetAll();    
            return View(events);
        }

        public IActionResult Create()
        {
            var curUserId = _httpContextAccessor.HttpContext.User.GetUserID();

            if (curUserId == null) return View("Error");

            var streetEventVM = new CreateEventViewModel
            {
                OwnerId = curUserId,
            };



            return View(streetEventVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEventViewModel createEventVM)
        {
            if (ModelState.IsValid)
            {
                var uploadeResult = await _photoService.AddPhotoAsync(createEventVM.Image);

                var streetEvent = new Event
                {
                    Title = createEventVM.Title,
                    Description = createEventVM.Description,
                    Image = uploadeResult.Url.ToString(),
                    Address = new Address
                    {
                        City = createEventVM.City,
                        PostalCode = createEventVM.PostalCode,
                        street = createEventVM.Street,
                        Region = createEventVM.Region,
                        Country = createEventVM.Country,

                    },
                    Date = createEventVM.Date,
                    OwnerId = createEventVM.OwnerId,
                    EventCategory = createEventVM.EventCategory
                    
                    

                };
                _eventReposiroty.Add(streetEvent);
                return RedirectToAction("Index");


            }
            else
            {
                ModelState.AddModelError("", "Photo Upload Error");
                return View(createEventVM);
            }
        }

        public async Task<IActionResult> Detail(int id)
        {

            Console.WriteLine(id);
            Event streetEvent = await _eventReposiroty.GetByIdAsync(id);

            if (streetEvent == null)
            {
                // Handle event not found, e.g., return a "Not Found" view
                return NotFound();
            }

            var eventDetailVM = new EventDetailViewModel
            {
                Id = streetEvent.Id,
                Title = streetEvent.Title,
                Date = streetEvent.Date,
                Description = streetEvent.Description,
                Street = streetEvent.Address?.street,
                City = streetEvent.Address?.street,
                Region = streetEvent.Address?.Region,
                PostalCode = streetEvent.Address?.PostalCode,
                OwnerName = streetEvent.Owner?.UserName,
                Image = streetEvent.Image,
                EventCategory = streetEvent.EventCategory,
                FollowerCount = streetEvent.Followers?.Count(),
                Comments = streetEvent.Comments

            };
            return View(eventDetailVM);
        }
        [HttpPost]
        public async Task <IActionResult> Attend (AttendEventViewModel attendEventVM)
        {
            var streetEvent = await _eventReposiroty.GetByIdAsync(attendEventVM.EventId);

            var curUserId = _httpContextAccessor.HttpContext.User.GetUserID();

            var curUser = await _eventReposiroty.GetCurrentUser(curUserId);


            if (streetEvent != null && curUser != null && !streetEvent.Followers.Contains(curUser)){
                streetEvent.Followers.Add(curUser);
                _eventReposiroty.Update(streetEvent);
                
            }

            return RedirectToAction("Detail", new { id = attendEventVM.EventId });

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
                CommentorId = curUserId,
                Commentor = curUser,
                EventId = addCommentVM.EventId,

            };
            var streetEvent = await _eventReposiroty.GetByIdAsync(addCommentVM.EventId);
            streetEvent.Comments.Add(eventComment);
            _eventReposiroty.Update(streetEvent);

            return RedirectToAction("Detail", new { id = addCommentVM.EventId });

        }
    }
}
