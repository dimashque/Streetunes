using Streetunes.Models;

namespace Streetunes.ViewModel
{
    public class UserDetailViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }
        public string? ProfileImageUrl { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }
        public ICollection<Event>? Events { get; set; } = new List<Event>();  // Many-to-Many Relationship

        public ICollection<Event>? CreatedEvents { get; set; } = new List<Event>();
    }
}
