using System.ComponentModel.DataAnnotations;

namespace Verse_Comment.DTOs
{
    public class CreateCommentRequest
    {
        [Required]
        public string Content { get; set; }

        [Required]
        public int PostId { get; set; }

        public int? ParentCommentId { get; set; }
    }
}
