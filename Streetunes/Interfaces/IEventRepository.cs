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

        bool Add(Event streetEvent);

        bool Update(Event streetEvent);
        bool Delete(Event streetEvent);

        bool Save();
    }
}
