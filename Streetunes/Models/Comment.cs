using System.ComponentModel.DataAnnotations.Schema;

namespace Streetunes.Models
{
    public class Comment
    {

        public int Id { get; set; }
        public string CommentText { get; set; }
        public DateTime CreatedDate { get; set; }
        [ForeignKey("AppUser")]
        public string CommenterId { get; set; }

        public AppUser Commenter { get; set; }

        [ForeignKey("Event")]
        public int EventId { get; set; }
    }
}
