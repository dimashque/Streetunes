using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Streetunes.Models;

namespace Streetunes.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventApiController : ControllerBase
    {
        private readonly IEventReposiroty _eventRepository;

        public EventApiController(IEventReposiroty eventRepository)
        {
            _eventRepository = eventRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            var events = await _eventRepository.GetAll();
            return Ok(events);
                

        }

        [HttpGet("{id}")]
        public  async Task <ActionResult<Event>> GetEvent(int id)
        {
            var streetevent =  await _eventRepository.GetByIdAsync(id);
            return Ok(streetevent);
        }
    }
}
