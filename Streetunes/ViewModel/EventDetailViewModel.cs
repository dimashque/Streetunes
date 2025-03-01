using Streetunes.Data.Enum;
using Streetunes.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Streetunes.ViewModel
{
    public class EventDetailViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public DateTime Date { get; set; }

        public string Image { get; set; }

        public string? City { get; set; }
        public string? Region { get; set; }
        public string? PostalCode { get; set; }
        public string? street { get; set; }

        public string OwnerName { get; set; }

        public EventCategory EventCategory { get; set; }
        public string? Description { get; set; }

        public int? FollowerCount { get; set; }

        public ICollection<Comment>? Comments { get; set; }

       
            // Navigation property for the creator

       
    }

}

