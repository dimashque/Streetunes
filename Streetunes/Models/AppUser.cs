using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Net;

namespace Streetunes.Models
{
    public class AppUser  : IdentityUser 
    {

        public string? ProfileImageUrl { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }
         public ICollection<Event>? Events { get; set; }  // Many-to-Many Relationship
    }
}
