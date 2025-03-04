using System.ComponentModel.DataAnnotations.Schema;

namespace Streetunes.Models
{
    public class Comment
    {

        public int Id { get; set; }
        public string CommentText { get; set; }
        public DateTime CreatedDate { get; set; }
        [ForeignKey("AppUser")]
        public string CommentorId { get; set; }

        public AppUser Commentor { get; set; }

        [ForeignKey("Event")]
        public int EventId { get; set; }

        public Event StreetEvent { get; set; }
    }
}
