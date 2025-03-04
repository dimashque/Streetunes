using Streetunes.Data.Enum;
using Streetunes.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Streetunes.ViewModel
{
    public class CreateEventViewModel
    {
        
        public int Id { get; set; }
        public string Title { get; set; }

        public DateTime Date { get; set; }

        public IFormFile? Image { get; set; }

        public string? City { get; set; }
        public string? Region { get; set; }
        public string? PostalCode { get; set; }
        public string? Street { get; set; }

        public string? Country { get; set; }
        public EventCategory EventCategory { get; set; }
        public string? Description { get; set; }

        
        public string OwnerId { get; set; }   // Foreign Key for the creator of the club
        
    }
}
