using Streetunes.Models;

namespace Streetunes
{
    public interface IEventReposiroty
    {
        Task<IEnumerable<Event>> GetAll();

        Task<Event> GetByIdAsync(int id);

        Task<Event> GetByIdAsyncNoTracking(int id);

        Task<IEnumerable<Event>> GetEventByCity(string city);

        Task<IEnumerable<Event>> GetEventByPLZ(string plz);

        Task<IEnumerable<Event>> GetEventByRegion(string region);

        Task<AppUser> GetCurrentUser(string id);

        bool AddComment(Comment comment);

        

        bool Add(Event streetEvent);

        bool Update(Event streetEvent);
        bool Delete(Event streetEvent);

        bool Save();
    }
}
