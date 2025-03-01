using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Streetunes.Data.Enum;

namespace Streetunes.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }

        public DateTime Date { get; set; }

        public string Image { get; set; }

        [ForeignKey("Address")]
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public EventCategory EventCategory { get; set; }
        public string? Description { get; set; }

        [ForeignKey("AppUser")]
        public string OwnerId { get; set; }   // Foreign Key for the creator of the club
        public AppUser Owner { get; set; }    // Navigation property for the creator

        public ICollection<AppUser>? Followers { get; set; }  // Many-to-Many Relationship

        public ICollection<Comment>? Comments { get; set; }
    }
}
